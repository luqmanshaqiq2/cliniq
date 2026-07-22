using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.DATA;
using Cliniq.HELPER;
using Cliniq.MODELS;
using Cliniq.REPOS.IREPOS;
using Microsoft.EntityFrameworkCore;

namespace Cliniq.REPOS
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly CliniqDbContext _context;

        public InvoiceRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllAsync(InvoiceQueryObject query)
        {
            var invoices = _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.Appointment)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PatientId))
                invoices = invoices.Where(i => i.PatientId == query.PatientId);

            if (!string.IsNullOrWhiteSpace(query.DoctorId))
                invoices = invoices.Where(i => i.Appointment != null && i.Appointment.DoctorId == query.DoctorId);

            if (query.Status.HasValue)
                invoices = invoices.Where(i => i.Status == query.Status.Value);

            if (query.StartDate.HasValue)
                invoices = invoices.Where(i => i.IssuedDate >= query.StartDate.Value);

            if (query.EndDate.HasValue)
                invoices = invoices.Where(i => i.IssuedDate <= query.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                invoices = query.SortBy.ToLower() switch
                {
                    "issueddate" => query.IsDescending
                        ? invoices.OrderByDescending(i => i.IssuedDate)
                        : invoices.OrderBy(i => i.IssuedDate),
                    "duedate" => query.IsDescending
                        ? invoices.OrderByDescending(i => i.DueDate)
                        : invoices.OrderBy(i => i.DueDate),
                    "totalamount" => query.IsDescending
                        ? invoices.OrderByDescending(i => i.TotalAmount)
                        : invoices.OrderBy(i => i.TotalAmount),
                    "status" => query.IsDescending
                        ? invoices.OrderByDescending(i => i.Status)
                        : invoices.OrderBy(i => i.Status),
                    _ => invoices
                };
            }

            return await invoices.ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(string id)
        {
            return await _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.Appointment)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoiceModel)
        {
            await _context.Invoices.AddAsync(invoiceModel);
            await _context.SaveChangesAsync();
            return invoiceModel;
        }

        public async Task<Invoice?> UpdateInvoiceAsync(string id, Invoice invoiceModel)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return null;

            invoice.TotalAmount = invoiceModel.TotalAmount;
            invoice.DueDate = invoiceModel.DueDate;
            invoice.Status = invoiceModel.Status;
            invoice.PatientId = invoiceModel.PatientId;
            invoice.AppointmentId = invoiceModel.AppointmentId;

            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> DeleteInvoiceAsync(string id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}