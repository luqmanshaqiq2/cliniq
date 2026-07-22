using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cliniq.HELPER;
using Cliniq.MODELS;

namespace Cliniq.REPOS.IREPOS
{
    public interface IMedicalRecordRepository
    {
          Task<List<MedicalRecord>> GetAllAsync(MedicalRecordQueryObject query);
        Task<MedicalRecord?> GetByIdAsync(string id);
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecordModel);
        Task<MedicalRecord?> UpdateMedicalRecordAsync(string id, MedicalRecord medicalRecordModel);
        Task<bool> DeleteMedicalRecordAsync(string id);
    }
}