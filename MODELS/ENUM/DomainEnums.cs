namespace Cliniq.MODELS.ENUM;

public enum UserRole
{
    Admin,
    Doctor,
    Patient,
    Receptionist
}

public enum AppointmentStatus
{
    Scheduled,
    Confirmed,
    Completed,
    Cancelled,
    NoShow,
    InProgress
}

public enum PaymentStatus
{
    Pending,
    PartiallyPaid,
    Paid
}

public enum TreatmentType
{
    Medication = 1,
    Surgery = 2
}
