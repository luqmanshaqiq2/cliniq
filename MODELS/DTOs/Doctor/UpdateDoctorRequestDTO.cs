using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS.DTOs.Doctor
{
    public class UpdateDoctorRequestDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Specialization { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public double? ConsultationFee { get; set; }
    }
}