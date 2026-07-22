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
    public class PaymentRepository : IPaymentRepository
    {
         private readonly CliniqDbContext _context;

        public PaymentRepository(CliniqDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllAsync(PaymentQueryObject query)
        {
            var payments = _context.Payments
                .Include(p => p.Invoice)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.InvoiceId))
                payments = payments.Where(p => p.InvoiceId == query.InvoiceId);

            if (!string.IsNullOrWhiteSpace(query.PatientId))
                payments = payments.Where(p => p.Invoice.PatientId == query.PatientId);

            if (query.Method.HasValue)
                payments = payments.Where(p => p.Method == query.Method.Value);

            if (query.Status.HasValue)
                payments = payments.Where(p => p.Status == query.Status.Value);

            if (query.StartDate.HasValue)
                payments = payments.Where(p => p.PaymentDate >= query.StartDate.Value);

            if (query.EndDate.HasValue)
                payments = payments.Where(p => p.PaymentDate <= query.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                payments = query.SortBy.ToLower() switch
                {
                    "paymentdate" => query.IsDescending
                        ? payments.OrderByDescending(p => p.PaymentDate)
                        : payments.OrderBy(p => p.PaymentDate),
                    "amount" => query.IsDescending
                        ? payments.OrderByDescending(p => p.Amount)
                        : payments.OrderBy(p => p.Amount),
                    _ => payments
                };
            }

            return await payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(string id)
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment> CreatePaymentAsync(Payment paymentModel)
        {
            await _context.Payments.AddAsync(paymentModel);
            await _context.SaveChangesAsync();
            return paymentModel;
        }

        public async Task<Payment?> UpdatePaymentAsync(string id, Payment paymentModel)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            payment.Amount = paymentModel.Amount;
            payment.PaymentDate = paymentModel.PaymentDate;
            payment.Method = paymentModel.Method;
            payment.Status = paymentModel.Status;

            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> DeletePaymentAsync(string id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}