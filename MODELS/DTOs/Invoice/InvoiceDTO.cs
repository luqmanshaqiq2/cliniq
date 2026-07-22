using Cliniq.MODELS.DTOs.Payment;
using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Invoice
{
    public class InvoiceDTO
    {
        public string Id { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public int PatientId { get; set; }
        public string AppointmentId { get; set; } = string.Empty;
        public List<PaymentDTO>? Payments { get; set; }
    }
}
