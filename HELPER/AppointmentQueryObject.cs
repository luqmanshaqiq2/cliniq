using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUM;

namespace Cliniq.HELPER
{
    public class AppointmentQueryObject
    {
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public AppointmentStatus? Status { get; set; }
        public DateTime? FromUtc { get; set; }
        public DateTime? ToUtc { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
