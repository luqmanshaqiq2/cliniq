using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cliniq.DATA;
using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.REPOS.IREPOS;

namespace Cliniq.REPOS
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly CliniqDbContext _context;

        public AppointmentRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAsync(AppointmentQueryObject query)
        {
            var appointments = _context.Appointments.AsQueryable();

            // Apply filtering based on the query object
           if (!string.IsNullOrEmpty(query.DoctorId))
           {
                appointments = appointments.Where(a => a.DoctorId == query.DoctorId);
           }

           if (!string.IsNullOrEmpty(query.PatientId))
            {
                appointments = appointments.Where(a => a.PatientId == query.PatientId);
            }
            if (!string.IsNullOrEmpty(query.Status))
            {
                appointments = appointments.Where(a => a.Status == query.Status);
            }

            // Apply sorting based on the query object
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "datetime":
                        appointments = query.SortDescending ? appointments.OrderByDescending(a => a.AppointmentDate) : appointments.OrderBy(a => a.AppointmentDate);
                        break;
                    case "status":
                        appointments = query.SortDescending ? appointments.OrderByDescending(a => a.Status) : appointments.OrderBy(a => a.Status);
                        break;
                    default:
                        break;
                }
            }

            return await appointments.ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(string id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointmentModel)
        {
            await _context.Appointments.AddAsync(appointmentModel);
            await _context.SaveChangesAsync();
            return appointmentModel;
        }

        public async Task<Appointment?> UpdateAppointmentAsync(string id, Appointment appointmentModel)
        {
            var existingAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (existingAppointment == null)
            {
                return null;
            }

            // Update the properties of the existing appointment
            existingAppointment.AppointmentDate = appointmentModel.AppointmentDate;
            existingAppointment.Reason = appointmentModel.Reason;
            existingAppointment.Notes = appointmentModel.Notes;
            existingAppointment.Status = appointmentModel.Status;
            existingAppointment.DoctorId = appointmentModel.DoctorId;
            existingAppointment.PatientId = appointmentModel.PatientId;


            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<Appointment?> DeleteAppointmentAsync(string id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                return null;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
    }
    }
