using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.HELPER
{
    public class PatientQueryObject
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
