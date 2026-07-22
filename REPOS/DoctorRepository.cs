using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cliniq.MODELS;
using Cliniq.REPOS.IREPOS;
using Cliniq.DATA;
using Cliniq.HELPER;

namespace Cliniq.REPOS
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly CliniqDbContext _context;

        public DoctorRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync(DoctorQueryObject query)
        {
            var doctors = _context.Doctors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.FirstName))
                doctors = doctors.Where(d => d.FirstName.Contains(query.FirstName));

            if (!string.IsNullOrWhiteSpace(query.LastName))
                doctors = doctors.Where(d => d.LastName.Contains(query.LastName));

            if (!string.IsNullOrWhiteSpace(query.Specialization))
                doctors = doctors.Where(d => d.Specialization == query.Specialization);

             if (!string.IsNullOrWhiteSpace(query.SortBy))
                {
                    doctors = query.SortBy.ToLower() switch
                    {
                        "firstname" => query.IsDescending
                            ? doctors.OrderByDescending(d => d.FirstName)
                            : doctors.OrderBy(d => d.FirstName),
                        "lastname" => query.IsDescending
                            ? doctors.OrderByDescending(d => d.LastName)
                            : doctors.OrderBy(d => d.LastName),
                        "specialization" => query.IsDescending
                            ? doctors.OrderByDescending(d => d.Specialization)
                            : doctors.OrderBy(d => d.Specialization),
                        _ => doctors
                    };
                }

            return await doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(string id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor?> GetByEmailAsync(string email)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email);
        }

        public async Task<Doctor?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Phone == phoneNumber);
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctorModel)
        {
            await _context.Doctors.AddAsync(doctorModel);
            await _context.SaveChangesAsync();
            return doctorModel;
        }

        public async Task<Doctor?> UpdateDoctorAsync(string id, Doctor doctorModel)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existingDoctor == null) return null;

            existingDoctor.FirstName = doctorModel.FirstName;
            existingDoctor.LastName = doctorModel.LastName;
            existingDoctor.Specialization = doctorModel.Specialization;
            existingDoctor.Phone = doctorModel.Phone;
            existingDoctor.Email = doctorModel.Email;
            existingDoctor.ConsultationFee = doctorModel.ConsultationFee;

            await _context.SaveChangesAsync();
            return existingDoctor;
        }

        public async Task<Doctor?> DeleteDoctorAsync(string id)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existingDoctor == null) return null;

            _context.Doctors.Remove(existingDoctor);
            await _context.SaveChangesAsync();
            return existingDoctor;
        }
    }
}