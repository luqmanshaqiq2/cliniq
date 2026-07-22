namespace Cliniq.MODELS.DTOs.Appointment
{
    public class AppointmentDTO
    {
        public string Id { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
