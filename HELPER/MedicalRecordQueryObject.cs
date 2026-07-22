using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.HELPER
{
    public class MedicalRecordQueryObject
    {
         public string? PatientId { get; set; }
        public string? DoctorId { get; set; }
        public string? AppointmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}