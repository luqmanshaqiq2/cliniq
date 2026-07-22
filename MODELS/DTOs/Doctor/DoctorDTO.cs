namespace Cliniq.MODELS.DTOs.Doctor
{
    public class DoctorDTO
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double ConsultationFee { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
