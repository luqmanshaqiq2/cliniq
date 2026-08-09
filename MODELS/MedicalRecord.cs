using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUM;

namespace Cliniq.MODELS
{
    public class MedicalRecord
    {
          public int Id { get; set; }

    public int PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    public int AppointmentId { get; set; }

    public Appointment Appointment { get; set; } = null!;

    public string Diagnosis { get; set; } = string.Empty;

    public string? Prescription { get; set; }

    public TreatmentType TreatmentType { get; set; }

    public string? SurgeryDetails { get; set; }

    public int? ApprovedByDoctorId { get; set; }

    public Doctor? ApprovedByDoctor { get; set; }

    public DateTime? ApprovedAtUtc { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
