namespace Cliniq.MODELS.DTOs.Prescription
{
    public class PrescriptionDTO
    {
        public string Id { get; set; } = string.Empty;
        public string MedicalRecordId { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public DateTime PrescriptionDate { get; set; }
    }
}
