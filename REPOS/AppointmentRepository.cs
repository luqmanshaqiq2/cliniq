using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.Data;
using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.MODELS.DTO;
using Cliniq.MODELS.ENUM;
using Cliniq.REPOS.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.REPOS
{
    public class AppointmentRepository : IAppointmentRepository
    {
         private readonly CliniqDbContext _context;

    public AppointmentRepository(CliniqDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<Appointment>> GetAllAsync(AppointmentQueryObject query)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .AsQueryable();

        if (query.PatientId.HasValue)
            appointments = appointments.Where(a => a.PatientId == query.PatientId);

        if (query.DoctorId.HasValue)
            appointments = appointments.Where(a => a.DoctorId == query.DoctorId);

        if (query.Status.HasValue)
            appointments = appointments.Where(a => a.Status == query.Status);

        // All comparisons happen in UTC — ScheduledAtUtc is stored as UTC (Kind=Utc).
        if (query.FromUtc.HasValue)
            appointments = appointments.Where(a => a.ScheduledAtUtc >= query.FromUtc);

        if (query.ToUtc.HasValue)
            appointments = appointments.Where(a => a.ScheduledAtUtc <= query.ToUtc);

        appointments = appointments.OrderBy(a => a.ScheduledAtUtc);

        var totalCount = await appointments.CountAsync();

        var items = await appointments
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PageResult<Appointment>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<Appointment?> GetByIdAsync(int id) =>
        await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> UpdateStatusAsync(int id, AppointmentStatus status)
    {
        var existing = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (existing is null) return null;

        if (existing.Status is AppointmentStatus.Completed
            or AppointmentStatus.Cancelled
            or AppointmentStatus.NoShow)
            throw new InvalidOperationException("A completed, cancelled, or missed appointment cannot be reopened.");

        existing.Status = status;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<int?> GetDoctorIdForUserAsync(int userId) =>
        await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.DoctorId)
            .FirstOrDefaultAsync();

    public async Task<MedicalRecord?> GetTreatmentAsync(int appointmentId) =>
        await _context.MedicalRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.AppointmentId == appointmentId);

    public async Task<MedicalRecord> RecordTreatmentAsync(
        int appointmentId,
        int doctorId,
        int performedByUserId,
        CreateTreatmentDto treatment)
    {
        var appointment = await _context.Appointments
            .Include(a => a.MedicalRecord)
            .FirstOrDefaultAsync(a => a.Id == appointmentId)
            ?? throw new KeyNotFoundException("Appointment was not found.");

        if (appointment.DoctorId != doctorId)
            throw new UnauthorizedAccessException("Only the doctor assigned to this appointment can approve treatment.");

        if (appointment.Status != AppointmentStatus.InProgress)
            throw new InvalidOperationException(
                "Treatment can be approved only after the appointment has been marked in progress.");

        if (appointment.MedicalRecord is not null)
            throw new InvalidOperationException("Treatment has already been recorded for this appointment.");

        var record = new MedicalRecord
        {
            AppointmentId = appointment.Id,
            PatientId = appointment.PatientId,
            Diagnosis = treatment.Diagnosis.Trim(),
            TreatmentType = treatment.TreatmentType,
            Prescription = treatment.TreatmentType == TreatmentType.Medication
                ? treatment.Prescription?.Trim()
                : null,
            SurgeryDetails = treatment.TreatmentType == TreatmentType.Surgery
                ? treatment.SurgeryDetails?.Trim()
                : null,
            Notes = treatment.Notes?.Trim(),
            ApprovedByDoctorId = doctorId,
            ApprovedAtUtc = DateTime.UtcNow
        };

        appointment.Status = AppointmentStatus.Completed;
        _context.MedicalRecords.Add(record);
        _context.AuditLogs.Add(new AuditLog
        {
            Action = "TreatmentApproved",
            EntityName = nameof(Appointment),
            EntityId = appointment.Id,
            PerformedByUserId = performedByUserId,
            Details = $"{treatment.TreatmentType} treatment approved for medical record."
        });

        await _context.SaveChangesAsync();
        return record;
    }

    private static readonly TimeSpan DoubleBookingBuffer = TimeSpan.FromMinutes(30);

    public async Task<bool> IsDoubleBookedAsync(int doctorId, DateTime scheduledAtUtc)
    {
        // Ensure Kind=Utc so EF/SqlServer comparisons stay consistent regardless
        // of how the caller constructed the DateTime (this was an earlier bug).
        var utc = DateTime.SpecifyKind(scheduledAtUtc, DateTimeKind.Utc);
        var startWindow = utc - DoubleBookingBuffer;
        var endWindow = utc + DoubleBookingBuffer;

        return await _context.Appointments.AnyAsync(a =>
            a.DoctorId == doctorId &&
            a.Status != AppointmentStatus.Cancelled &&
            a.Status != AppointmentStatus.NoShow &&
            a.ScheduledAtUtc >= startWindow &&
            a.ScheduledAtUtc <= endWindow);
    }

    public async Task<bool> IsAvailableAsync(int doctorId, DateTime scheduledAtUtc)
    {
        var utc = DateTime.SpecifyKind(scheduledAtUtc, DateTimeKind.Utc);

        return await _context.AvailabilitySlots.AnyAsync(s =>
            s.DoctorId == doctorId &&
            !s.IsBooked &&
            s.StartTimeUtc <= utc &&
            s.EndTimeUtc > utc);
    }
    }
}
