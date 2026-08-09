
using Cliniq.MODELS;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.Data;

public class CliniqDbContext : DbContext
{
    public CliniqDbContext(DbContextOptions<CliniqDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<AvailabilitySlot> AvailabilitySlots => Set<AvailabilitySlot>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ApplicationUser -> Patient (optional 1:1)
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Patient)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.PatientId)
            .OnDelete(DeleteBehavior.SetNull);

        // ApplicationUser -> Doctor (optional 1:1)
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Doctor)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.DoctorId)
            .OnDelete(DeleteBehavior.SetNull);

        // Doctor -> AvailabilitySlots (1:M)
        modelBuilder.Entity<AvailabilitySlot>()
            .HasOne(s => s.Doctor)
            .WithMany(d => d.AvailabilitySlots)
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Patient -> Appointments (1:M)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Doctor -> Appointments (1:M)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // AvailabilitySlot -> Appointment (1:0..1)
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.AvailabilitySlot)
            .WithOne()
            .HasForeignKey<Appointment>(a => a.AvailabilitySlotId)
            .OnDelete(DeleteBehavior.SetNull);

        // Patient -> MedicalRecords (1:M)
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.MedicalRecords)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment -> MedicalRecord (1:1)
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(m => m.Appointment)
            .WithOne(a => a.MedicalRecord)
            .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // MedicalRecord -> approving Doctor (1:M)
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(m => m.ApprovedByDoctor)
            .WithMany()
            .HasForeignKey(m => m.ApprovedByDoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment -> Invoice (1:1)
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Appointment)
            .WithOne(a => a.Invoice)
            .HasForeignKey<Invoice>(i => i.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Invoice -> Payments (1:M)
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CliniqDbContext).Assembly);
    }
}
