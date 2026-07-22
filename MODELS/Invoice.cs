using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUMs;

namespace Cliniq.MODELS
{
    public class Invoice
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(30);
        public decimal TotalAmount { get; set; } = 0;
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

        public string PatientId { get; set; } = string.Empty;
        public Patient Patient { get; set; } = null!;

        public string AppointmentId { get; set; } = string.Empty;
        public Appointment? Appointment { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}