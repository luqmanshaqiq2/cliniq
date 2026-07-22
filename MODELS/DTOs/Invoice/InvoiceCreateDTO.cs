using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Invoice
{
    public class InvoiceCreateDTO
    {
        public int PatientId { get; set; }
        public string AppointmentId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
