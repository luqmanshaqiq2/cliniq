using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Patient
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Male;
        public string Address { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
