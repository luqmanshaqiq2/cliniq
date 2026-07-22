using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Payment
{
    public class PaymentDTO
    {
        public string Id { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string InvoiceId { get; set; } = string.Empty;
    }
}
