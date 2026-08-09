using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUM;

namespace Cliniq.MODELS
{
    public class Appointment
    {
        public int Id { get; set; }

    public int PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public int? AvailabilitySlotId { get; set; }

    public AvailabilitySlot? AvailabilitySlot { get; set; }

    public DateTime ScheduledAtUtc { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public MedicalRecord? MedicalRecord { get; set; }

    public Invoice? Invoice { get; set; }
    }
}