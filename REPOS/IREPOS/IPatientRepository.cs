using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync(PatientQueryObject query);
        Task<Patient?> GetByIdAsync(string id);
        Task<Patient?> GetByEmailAsync(string email);
        Task<Patient?> GetByContactNumberAsync(string contactNumber);
        Task<Patient> CreatePatientAsync(Patient patientModel);
        Task<Patient?> UpdatePatientAsync(string id, Patient patientModel);
        Task<Patient?> DeletePatientAsync(string id);
    }
}