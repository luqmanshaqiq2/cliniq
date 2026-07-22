using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUMs;

namespace Cliniq.HELPER
{
    public class AppointmentQueryObject
    {
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }
        public string? Status { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public bool SortDescending
        {
            get => IsDescending;
            set => IsDescending = value;
        }
    }
}