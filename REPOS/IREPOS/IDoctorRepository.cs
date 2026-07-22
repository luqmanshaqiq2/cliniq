using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync(DoctorQueryObject query);
        Task<Doctor?> GetByIdAsync(string id);
        Task<Doctor?> GetByEmailAsync(string email);
        Task<Doctor?> GetByPhoneNumberAsync(string phoneNumber);
        Task<Doctor> CreateDoctorAsync(Doctor doctorModel);
        Task<Doctor?> UpdateDoctorAsync(string id, Doctor doctorModel);
        Task<Doctor?> DeleteDoctorAsync(string id);
    }
}