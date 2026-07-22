using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync(InvoiceQueryObject query);
        Task<Invoice?> GetByIdAsync(string id);
        Task<Invoice> CreateInvoiceAsync(Invoice invoiceModel);
        Task<Invoice> UpdateInvoiceAsync(string id, Invoice invoiceModel);
        Task<bool> DeleteInvoiceAsync(string id);
    }
}