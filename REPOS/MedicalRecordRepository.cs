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
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly CliniqDbContext _context;

        public MedicalRecordRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalRecord>> GetAllAsync(MedicalRecordQueryObject query)
        {
            var records = _context.MedicalRecords
                .Include(r => r.Patient)
                .Include(r => r.Doctor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PatientId))
                records = records.Where(r => r.PatientId == query.PatientId);

            if (!string.IsNullOrWhiteSpace(query.DoctorId))
                records = records.Where(r => r.DoctorId == query.DoctorId);

            if (query.StartDate.HasValue)
                records = records.Where(r => r.VisitDate >= query.StartDate.Value);

            if (query.EndDate.HasValue)
                records = records.Where(r => r.VisitDate <= query.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                records = query.SortBy.ToLower() switch
                {
                    "recorddate" => query.IsDescending
                        ? records.OrderByDescending(r => r.VisitDate)
                        : records.OrderBy(r => r.VisitDate),
                    _ => records
                };
            }

            return await records.ToListAsync();
        }

        public async Task<MedicalRecord?> GetByIdAsync(string id)
        {
            return await _context.MedicalRecords
                .Include(r => r.Patient)
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecordModel)
        {
            await _context.MedicalRecords.AddAsync(medicalRecordModel);
            await _context.SaveChangesAsync();
            return medicalRecordModel;
        }

        public async Task<MedicalRecord?> UpdateMedicalRecordAsync(string id, MedicalRecord medicalRecordModel)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return null;

            record.Diagnosis = medicalRecordModel.Diagnosis;
            record.Symptoms = medicalRecordModel.Symptoms;
            record.Treatment = medicalRecordModel.Treatment;
            record.Notes = medicalRecordModel.Notes;

            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteMedicalRecordAsync(string id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return false;

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}