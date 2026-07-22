using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.DATA;
using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.REPOS.IREPOS;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.REPOS
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly CliniqDbContext _context;

        public PrescriptionRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prescription>> GetAllAsync(PrescriptionQueryObject query)
        {
            var prescriptions = _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.MedicalRecord)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PatientId))
                prescriptions = prescriptions.Where(p => p.PatientId == query.PatientId);

            if (!string.IsNullOrWhiteSpace(query.DoctorId))
                prescriptions = prescriptions.Where(p => p.DoctorId == query.DoctorId);

            if (!string.IsNullOrWhiteSpace(query.MedicalRecordId))
                prescriptions = prescriptions.Where(p => p.MedicalRecordId == query.MedicalRecordId);

            if (!string.IsNullOrWhiteSpace(query.MedicineName))
                prescriptions = prescriptions.Where(p => p.MedicineName.Contains(query.MedicineName));

            if (query.StartDate.HasValue)
                prescriptions = prescriptions.Where(p => p.PrescriptionDate >= query.StartDate.Value);

            if (query.EndDate.HasValue)
                prescriptions = prescriptions.Where(p => p.PrescriptionDate <= query.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                prescriptions = query.SortBy.ToLower() switch
                {
                    "prescriptiondate" => query.IsDescending
                        ? prescriptions.OrderByDescending(p => p.PrescriptionDate)
                        : prescriptions.OrderBy(p => p.PrescriptionDate),
                    "medicinename" => query.IsDescending
                        ? prescriptions.OrderByDescending(p => p.MedicineName)
                        : prescriptions.OrderBy(p => p.MedicineName),
                    _ => prescriptions
                };
            }

            return await prescriptions.ToListAsync();
        }

        public async Task<Prescription?> GetByIdAsync(string id)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.MedicalRecord)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescriptionModel)
        {
            await _context.Prescriptions.AddAsync(prescriptionModel);
            await _context.SaveChangesAsync();
            return prescriptionModel;
        }

        public async Task<Prescription?> UpdatePrescriptionAsync(string id, Prescription prescriptionModel)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return null;

            prescription.MedicineName = prescriptionModel.MedicineName;
            prescription.Dosage = prescriptionModel.Dosage;
            prescription.Duration = prescriptionModel.Duration;
            prescription.Instructions = prescriptionModel.Instructions;

            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<bool> DeletePrescriptionAsync(string id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return false;

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}