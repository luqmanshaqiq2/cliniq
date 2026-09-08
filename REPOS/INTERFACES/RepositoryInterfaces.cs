using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.MODELS.DTO;
using Cliniq.MODELS.ENUM;

namespace Cliniq.REPOS.INTERFACES;

public interface IPatientRepository
{
    Task<PageResult<Patient>> GetAllAsync(PatientQueryObject query);
    Task<Patient?> GetByIdAsync(int id);
    Task<Patient> CreateAsync(Patient patient);
    Task<Patient?> UpdateAsync(int id, Patient patient);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> UpdateAsync(int id, Doctor doctor);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> LinkUserAsync(int doctorId, int userId);
}

public interface IAppointmentRepository
{
    Task<PageResult<Appointment>> GetAllAsync(AppointmentQueryObject query);
    Task<Appointment?> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateStatusAsync(int id, AppointmentStatus status);
    Task<int?> GetDoctorIdForUserAsync(int userId);
    Task<MedicalRecord?> GetTreatmentAsync(int appointmentId);
    Task<MedicalRecord> RecordTreatmentAsync(int appointmentId, int doctorId, int performedByUserId, CreateTreatmentDto treatment);
    Task<bool> IsDoubleBookedAsync(int doctorId, DateTime scheduledAtUtc);
    Task<bool> IsAvailableAsync(int doctorId, DateTime scheduledAtUtc);
}
