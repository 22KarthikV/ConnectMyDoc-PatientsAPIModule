using CMD.Domain.Entities;
using CMD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Infrastructure.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Task<Patient> UpdateAsync(Patient patient);
        Task<Patient> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync(int skip, int take);
        Task<int> GetTotalCountAsync();
        Task ActivateAsync(int id);
        Task InactivateAsync(int id);
        Task UpdatePrimaryClinicAsync(int patientId, Clinic clinic);
        Task UpdatePrimaryDoctorAsync(int patientId, int doctorId);
    }
}
