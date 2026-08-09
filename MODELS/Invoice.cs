using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUM;

namespace Cliniq.MODELS
{
    public class Invoice
    {
         public int Id { get; set; }

    public int AppointmentId { get; set; }

    public Appointment Appointment { get; set; } = null!;

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}