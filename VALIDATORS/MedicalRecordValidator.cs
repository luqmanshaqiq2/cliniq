using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.DTOs.MedicalRecord;
using FluentValidation;

namespace Cliniq.VALIDATORS
{
    public class CreateMedicalRecordValidator : AbstractValidator<CreateMedicalRecordDTO>
    {
        public CreateMedicalRecordValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0).WithMessage("PatientId is required.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.");

            RuleFor(x => x.AppointmentId)
                .NotEmpty().WithMessage("AppointmentId is required.");

            RuleFor(x => x.VisitDate)
                .NotEmpty().WithMessage("VisitDate is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("VisitDate cannot be in the future.");

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required.")
                .MaximumLength(500).WithMessage("Diagnosis must not exceed 500 characters.");

            RuleFor(x => x.Symptoms)
                .MaximumLength(500).WithMessage("Symptoms must not exceed 500 characters.");

            RuleFor(x => x.Treatment)
                .MaximumLength(500).WithMessage("Treatment must not exceed 500 characters.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");
        }
    }
}