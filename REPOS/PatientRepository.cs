using Cliniq.Data;
using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.MODELS.DTO;
using Cliniq.REPOS.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.REPOS;

public class PatientRepository : IPatientRepository
{
    private readonly CliniqDbContext _context;

    public PatientRepository(CliniqDbContext context)
    {
        _context = context;
    }

    public async Task<PageResult<Patient>> GetAllAsync(PatientQueryObject query)
    {
        var patients = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim().ToLower();
            patients = patients.Where(p =>
                p.FullName.ToLower().Contains(search) ||
                p.ContactNumber.Contains(search));
        }

        patients = query.SortBy?.ToLower() switch
        {
            "createdat" => query.IsDescending ? patients.OrderByDescending(p => p.CreatedAt) : patients.OrderBy(p => p.CreatedAt),
            _ => query.IsDescending ? patients.OrderByDescending(p => p.FullName) : patients.OrderBy(p => p.FullName)
        };

        var totalCount = await patients.CountAsync();

        var items = await patients
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PageResult<Patient>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<Patient?> GetByIdAsync(int id) =>
        await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Patient> CreateAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> UpdateAsync(int id, Patient patient)
    {
        var existing = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        if (existing is null) return null;

        existing.FullName = patient.FullName;
        existing.ContactNumber = patient.ContactNumber;
        existing.Email = patient.Email;
        existing.Address = patient.Address;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        if (existing is null) return false;

        _context.Patients.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Patients.AnyAsync(p => p.Id == id);
}
