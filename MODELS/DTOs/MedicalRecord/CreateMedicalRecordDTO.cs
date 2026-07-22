using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS.DTOs.MedicalRecord
{
    public class CreateMedicalRecordDTO
    {
        public int PatientId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public string AppointmentId { get; set; } = string.Empty;
        public DateTime VisitDate { get; set; } = DateTime.Now;

        public string Diagnosis { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}