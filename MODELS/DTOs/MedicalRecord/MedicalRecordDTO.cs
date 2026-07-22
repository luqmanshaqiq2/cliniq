using Cliniq.MODELS.DTOs.Prescription;

namespace Cliniq.MODELS.DTOs.MedicalRecord
{
    public class MedicalRecordDTO
    {
        public string Id { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public string AppointmentId { get; set; } = string.Empty;
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<PrescriptionDTO>? Prescriptions { get; set; }
    }
}
