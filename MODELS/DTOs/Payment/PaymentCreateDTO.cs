using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Payment
{
    public class PaymentCreateDTO
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string InvoiceId { get; set; } = string.Empty;
    }
}
