using FluentValidation;
using Cliniq.MODELS.DTOs.Patient;

namespace Cliniq.VALIDATORS
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientRequestDTO>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .MinimumLength(2)
        .MaximumLength(50)
        .Matches("^[A-Za-z]+$")
        .WithMessage("First name must contain only letters.");

        RuleFor(x => x.LastName)
        .NotEmpty()
        .MinimumLength(2)
        .MaximumLength(100)
        .Matches("^[A-Za-z]+$")
        .WithMessage("Last name must contain only letters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MinimumLength(5).MaximumLength(200);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Gender must be Male, Female, or Other.");

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob < DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past");

        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Contact number must be exactly 10 digits");

        RuleFor(x => x.EmergencyContact)
            .NotEmpty()
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Emergency contact must be exactly 10 digits");
        
        RuleFor(x => x.BloodGroup)
            .Must(bg => new[]
        {
            "A+", "A-", "B+", "B-",
            "AB+", "AB-", "O+", "O-"
        }.Contains(bg))
        .WithMessage("Invalid blood group.");
    }
}

public class UpdatePatientValidator : AbstractValidator<UpdatePatientRequestDTO>
{
    public UpdatePatientValidator()
    {
        
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .MinimumLength(2)
        .MaximumLength(50)
        .Matches("^[A-Za-z]+$")
        .WithMessage("First name must contain only letters.");

        RuleFor(x => x.LastName)
        .NotEmpty()
        .MinimumLength(2)
        .MaximumLength(100)
        .Matches("^[A-Za-z]+$")
        .WithMessage("Last name must contain only letters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MinimumLength(5).MaximumLength(200);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Gender must be Male, Female, or Other.");

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob < DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past");

        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Contact number must be exactly 10 digits");

        RuleFor(x => x.EmergencyContact)
            .NotEmpty()
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Emergency contact must be exactly 10 digits");
        
        RuleFor(x => x.BloodGroup)
            .Must(bg => new[]
        {
            "A+", "A-", "B+", "B-",
            "AB+", "AB-", "O+", "O-"
        }.Contains(bg))
        .WithMessage("Invalid blood group.");
    }
}
}