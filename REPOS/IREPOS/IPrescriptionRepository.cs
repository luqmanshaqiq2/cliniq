using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAllAsync(PrescriptionQueryObject query);
        Task<Prescription?> GetByIdAsync(string id);
        Task<Prescription> CreatePrescriptionAsync(Prescription prescriptionModel);
        Task<Prescription?> UpdatePrescriptionAsync(string id, Prescription prescriptionModel);
        Task<bool> DeletePrescriptionAsync(string id);
    }
}