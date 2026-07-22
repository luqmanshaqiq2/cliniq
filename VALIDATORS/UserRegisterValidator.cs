using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.MODELS.DTOs.Auth;
using FluentValidation;

namespace Cliniq.VALIDATORS
{
    public class UserRegisterValidator : AbstractValidator<RegisterDTO>
    {
        private static readonly string[] AllowedGenders = { "Male", "Female", "Other" };
        private static readonly string[] AllowedBloodGroups =
        {
            "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
        };

        public UserRegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(6, 100).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm your password")
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                .Matches(@"^[a-zA-Z\s'-]+$").WithMessage("First name can only contain letters, spaces, hyphens and apostrophes");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                .Matches(@"^[a-zA-Z\s'-]+$").WithMessage("Last name can only contain letters, spaces, hyphens and apostrophes");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past")
                .GreaterThan(DateTime.UtcNow.AddYears(-120)).WithMessage("Date of birth is not valid")
                .Must(BeAtLeast13YearsOld).WithMessage("User must be at least 13 years old to register");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => AllowedGenders.Contains(g, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Gender must be one of: {string.Join(", ", AllowedGenders)}");

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.BloodGroup)
                .Must(bg => AllowedBloodGroups.Contains(bg, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Blood group must be one of: {string.Join(", ", AllowedBloodGroups)}")
                .When(x => !string.IsNullOrWhiteSpace(x.BloodGroup));
        }

        //can a minor register? if yes check if the user is at least 13 years old
        private bool BeAtLeast13YearsOld(DateTime dob)
        {
            var age = DateTime.UtcNow.Year - dob.Year;
            if (dob.Date > DateTime.UtcNow.AddYears(-age)) age--;
            return age >= 13;
        }
    }
}