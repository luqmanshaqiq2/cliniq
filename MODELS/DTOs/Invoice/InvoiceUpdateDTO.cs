using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS.DTOs.Invoice
{
    public class InvoiceUpdateDTO
    {
        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public InvoiceStatus? Status { get; set; }
    }
}
