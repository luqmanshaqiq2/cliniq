using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cliniq.DATA;
using Cliniq.MODELS;
using Cliniq.REPOS.IREPOS;
using Cliniq.HELPER;

namespace Cliniq.REPOS
{
    public class PatientRepository : IPatientRepository
    {
        private readonly CliniqDbContext _context;

        public PatientRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllAsync(PatientQueryObject query)
        {
            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.FirstName))
            patients = patients.Where(p => p.FirstName.Contains(query.FirstName));

            if (!string.IsNullOrWhiteSpace(query.LastName))
                patients = patients.Where(p => p.LastName.Contains(query.LastName));

            if (query.Gender.HasValue)
                patients = patients.Where(p => p.Gender == query.Gender.Value);

            if (!string.IsNullOrWhiteSpace(query.BloodGroup))
                patients = patients.Where(p => p.BloodGroup == query.BloodGroup);

            if (!string.IsNullOrWhiteSpace(query.ContactNumber))
                patients = patients.Where(p => p.ContactNumber == query.ContactNumber);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                patients = query.SortBy.ToLower() switch
                {
                    "firstname" => query.IsDescending
                        ? patients.OrderByDescending(p => p.FirstName)
                        : patients.OrderBy(p => p.FirstName),
                    "lastname" => query.IsDescending
                        ? patients.OrderByDescending(p => p.LastName)
                        : patients.OrderBy(p => p.LastName),
                    "dateofbirth" => query.IsDescending
                        ? patients.OrderByDescending(p => p.DateOfBirth)
                        : patients.OrderBy(p => p.DateOfBirth),
                    _ => patients
                };
            }

            return await patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(string id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<Patient?> GetByEmailAsync(string email)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Patient?> GetByContactNumberAsync(string contactNumber)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.ContactNumber == contactNumber);
        }

        public async Task<Patient> CreatePatientAsync(Patient patientModel)
        {
            await _context.Patients.AddAsync(patientModel);
            await _context.SaveChangesAsync();
            return patientModel;
        }

        public async Task<Patient?> UpdatePatientAsync(string id, Patient patientModel)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (existingPatient == null) return null;

            existingPatient.FirstName = patientModel.FirstName;
            existingPatient.LastName = patientModel.LastName;
            existingPatient.Gender = patientModel.Gender;
            existingPatient.Address = patientModel.Address;
            existingPatient.Email = patientModel.Email;
            existingPatient.DateOfBirth = patientModel.DateOfBirth;
            existingPatient.ContactNumber = patientModel.ContactNumber;
            existingPatient.EmergencyContact = patientModel.EmergencyContact;
            existingPatient.BloodGroup = patientModel.BloodGroup;

            await _context.SaveChangesAsync();
            return existingPatient;
        }

        public async Task<Patient?> DeletePatientAsync(string id)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (existingPatient == null) return null;

            _context.Patients.Remove(existingPatient);
            await _context.SaveChangesAsync();
            return existingPatient;
        }
    }
}
