using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync(AppointmentQueryObject query);
        Task<Appointment?> GetByIdAsync(string id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointmentModel);
        Task<Appointment?> UpdateAppointmentAsync(string id, Appointment appointmentModel);
        Task<Appointment?> DeleteAppointmentAsync(string id);
    }
}