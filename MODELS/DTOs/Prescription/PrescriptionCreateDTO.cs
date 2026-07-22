using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS.DTOs.Prescription
{
    public class PrescriptionCreateDTO
    {
        [Required]
        public string MedicalRecordId { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string DoctorId { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string MedicineName { get; set; } = string.Empty;

        [StringLength(100)]
        public string Dosage { get; set; } = string.Empty;

        [StringLength(50)]
        public string Duration { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;
    }
}