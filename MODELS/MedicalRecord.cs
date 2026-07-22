using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS
{
    public class MedicalRecord
    {

        //VALIDATED
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PatientId { get; set; } = string.Empty;
        public Patient Patient { get; set; } = null!;
        public string DoctorId { get; set; } = string.Empty;
        public Doctor Doctor { get; set; } = null!;
        public string AppointmentId { get; set; } = string.Empty;
        public Appointment Appointment { get; set; } = null!;
        public DateTime VisitDate { get; set; } = DateTime.Now;

        public string Diagnosis { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // One-to-Many
        public ICollection<Prescription>? Prescriptions { get; set; }
    }
}