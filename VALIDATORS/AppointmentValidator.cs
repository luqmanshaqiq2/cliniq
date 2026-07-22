using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.DTOs.Appointment;
using FluentValidation;

namespace Cliniq.VALIDATORS
{
 
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDTO>
    {
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0)
            .WithMessage("Patient ID is required.");

        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor ID is required.");

        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Appointment date must be in the future.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Status is required and cannot exceed 20 characters.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Reason is required and cannot exceed 500 characters.");

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithMessage("Notes cannot exceed 1000 characters.");
    }
    }

    public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentDTO>
    {
        public UpdateAppointmentValidator()
        {
             RuleFor(x => x.PatientId)
            .GreaterThan(0)
            .WithMessage("Patient ID is required.");

        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor ID is required.");

        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Appointment date must be in the future.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Status is required and cannot exceed 20 characters.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Reason is required and cannot exceed 500 characters.");

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithMessage("Notes cannot exceed 1000 characters.");

     
        }
    }

}
