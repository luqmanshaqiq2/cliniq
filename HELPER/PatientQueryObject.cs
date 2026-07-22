using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.ENUMs;

namespace Cliniq.HELPER
{
    public class PatientQueryObject
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? ContactNumber { get; set; }

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}