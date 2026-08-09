using Cliniq.Data;
using Cliniq.MODELS;
using Cliniq.MODELS.ENUM;
using Cliniq.REPOS.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.REPOS;

public class DoctorRepository : IDoctorRepository
{
    private readonly CliniqDbContext _context;

    public DoctorRepository(CliniqDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllAsync() =>
        await _context.Doctors.AsNoTracking().ToListAsync();

    public async Task<Doctor?> GetByIdAsync(int id) =>
        await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Doctor> CreateAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor?> UpdateAsync(int id, Doctor doctor)
    {
        var existing = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null) return null;

        existing.FullName = doctor.FullName;
        existing.Specialization = doctor.Specialization;
        existing.ContactNumber = doctor.ContactNumber;
        existing.Email = doctor.Email;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null) return false;

        _context.Doctors.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Doctors.AnyAsync(d => d.Id == id);

    public async Task<bool> LinkUserAsync(int doctorId, int userId)
    {
        var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == doctorId);
        if (!doctorExists) return false;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new ArgumentException("User was not found.");

        if (user.Role != UserRole.Doctor)
            throw new ArgumentException("Only a user with the Doctor role can be linked to a doctor profile.");

        var assignedElsewhere = await _context.Users.AnyAsync(u =>
            u.Id != userId && u.DoctorId == doctorId);

        if (assignedElsewhere)
            throw new InvalidOperationException("This doctor profile is already linked to another user.");

        if (user.DoctorId.HasValue && user.DoctorId != doctorId)
            throw new InvalidOperationException("This user is already linked to another doctor profile.");

        user.DoctorId = doctorId;
        await _context.SaveChangesAsync();
        return true;
    }
}
