namespace Cliniq.MODELS.DTOs.MedicalRecord
{
    public class UpdateRecordRequestDTO
    {
        public string? Diagnosis { get; set; }
        public string? Symptoms { get; set; }
        public string? Treatment { get; set; }
        public string? Notes { get; set; }
    }
}
