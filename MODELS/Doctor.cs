using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS
{
    public class Doctor
    {
         public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string ContactNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();
    }
}