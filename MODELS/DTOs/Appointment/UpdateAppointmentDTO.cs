using System.ComponentModel.DataAnnotations;

namespace Cliniq.MODELS.DTOs.Appointment
{
    public class UpdateAppointmentDTO
    {
        public int PatientId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;

        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Notes { get; set; }
    }
}
