using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync(PaymentQueryObject query);
        Task<Payment?> GetByIdAsync(string id);
        Task<Payment> CreatePaymentAsync(Payment paymentModel);
        Task<Payment?> UpdatePaymentAsync(string id, Payment paymentModel);
        Task<bool> DeletePaymentAsync(string id);
    }
}