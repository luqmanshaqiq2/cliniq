using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS
{
    public class Payment
    {
           public int Id { get; set; }

    public int InvoiceId { get; set; }

    public Invoice Invoice { get; set; } = null!;

    public decimal AmountPaid { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }
}