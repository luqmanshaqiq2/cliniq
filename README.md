# Cliniq API

Cliniq is an ASP.NET Core Web API for managing patients, doctors, appointments, clinical records, invoices, and payments.
REFER THE PDF (IT LOOKS COOL & NICHE FOR ME TO LARP ABOUT THE PROJECT)

## Requirements

- .NET 10 SDK
- SQL Server or SQL Server Express

## Run locally

1. Update the `DefaultConnection` value in `appsettings.json` for your SQL Server instance.
2. Apply the Entity Framework migrations.
3. Start the API.

```powershell
dotnet ef database update
dotnet run
```

In the Development environment, Swagger is available at `/swagger`.

## Redis cache integration

This API uses Redis as a distributed cache for selected read operations. Doctor and patient read endpoints cache the fetched DTO results in Redis, which reduces repeated database queries for frequently requested records.

- Redis is configured via `Redis:Configuration` and `Redis:InstanceName` in `appsettings.json`.
- The API registers `AddStackExchangeRedisCache(...)` in `Program.cs`.
- Cached doctor records are invalidated when doctors are created, updated, or deleted.
- Cached patient records are invalidated when a patient is updated or deleted.

If Redis is not available, the application will still function but without distributed caching benefits.

## Production deployment

## Production deployment

- Use `appsettings.Production.json` or environment variables to override the production connection string and JWT secret.
- Do not leave `Jwt:Key` or production DB credentials in source control.
- Run EF migrations before starting the API in production, for example:

```powershell
dotnet ef database update --environment Production
```

- Use a proper secret store or environment variables for `Jwt:Key`, `ConnectionStrings:DefaultConnection`, and other sensitive configuration.
- Confirm that HTTPS and HSTS are enabled in production.

## Authentication

Send a JWT in the `Authorization` request header for protected endpoints:

```text
Authorization: Bearer <token>
```

Roles used by the API are `Admin`, `Doctor`, `Patient`, and `Receptionist`.

## Appointment workflow

1. Create an appointment.
2. Update its status as the patient arrives and the visit progresses.
3. Mark it `Completed` after the consultation.
4. Create one medical record and, when applicable, one invoice for the completed appointment.
5. Record payments against the invoice until it is paid.

### Appointment scheduling rules

- Appointments must be created inside an existing doctor availability slot.
- A doctor cannot be double-booked within a 30-minute buffer around an existing scheduled appointment.
- This prevents overlapping visits and also stops back-to-back bookings that are too close together for the doctor to prepare or wrap up.
- Cancelled and NoShow appointments are ignored for this conflict check.

Appointment status values are:

| Value | Status |
| --- | --- |
| 0 | Scheduled |
| 1 | Confirmed |
| 2 | Completed |
| 3 | Cancelled |
| 4 | NoShow |
| 5 | InProgress |

## Clinical treatment approval

The assigned doctor can record a treatment decision while an appointment is `InProgress`. The action completes the appointment, saves one medical record, and records an audit entry.

`POST /api/appointments/{id}/treatment` requires the `Doctor` role and a doctor user account linked to the appointment's doctor profile.

Medication treatment example:

```json
{
  "diagnosis": "Seasonal allergic rhinitis",
  "treatmentType": 1,
  "prescription": "Cetirizine 10mg once daily for 7 days",
  "notes": "Return if symptoms persist."
}
```

Surgical treatment example:

```json
{
  "diagnosis": "Symptomatic gallstones",
  "treatmentType": 2,
  "surgeryDetails": "Refer for laparoscopic cholecystectomy assessment"
}
```

Treatment types are `1` for medication and `2` for surgery. A treatment decision records the clinical plan; it does not mean medicine was dispensed or surgery was performed.

An administrator links a doctor user to a doctor profile with:

```http
PUT /api/doctors/{doctorId}/user
```

```json
{
  "userId": 12
}
```

## Medical records, invoices, and payments

All three are sub-resources. There are no standalone medical-record or payment endpoints.

### Create a medical record

```http
POST /api/appointments/{id}/medical-record
```

```json
{
  "diagnosis": "Viral upper respiratory infection",
  "prescription": "Paracetamol 500mg as needed",
  "notes": "Rest and hydration advised."
}
```

Requires `Admin`, `Doctor`, or `Receptionist`. The appointment must be `Completed`, and only one medical record can exist for each appointment. The patient and appointment are taken from the route and stored appointment, never from the request body.

### Create an invoice

```http
POST /api/appointments/{id}/invoice
```

```json
{
  "amount": 2500.00
}
```

Requires `Admin`, `Doctor`, or `Receptionist`. Only one invoice can exist per appointment. New invoices start with the `Pending` payment status.

### Record a payment

```http
POST /api/invoices/{invoiceId}/payments
```

```json
{
  "amountPaid": 1000.00,
  "paymentMethod": "Card"
}
```

Requires `Admin`, `Doctor`, or `Receptionist`. Payments cannot exceed the remaining invoice balance. The response is the updated invoice including all of its payments. Its status becomes `PartiallyPaid` or `Paid` when appropriate.

## Errors

Business-rule conflicts return `409 Conflict`, such as:

- creating a second medical record or invoice for an appointment;
- creating a medical record before an appointment is completed;
- paying more than the remaining invoice balance;
- attempting to record treatment twice.

Validation errors return `400 Bad Request`, missing resources return `404 Not Found`, and authorization failures return `401 Unauthorized` or `403 Forbidden`.
