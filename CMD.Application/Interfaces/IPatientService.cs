using CMD.Application.DTOs;
using CMD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> AddPatientAsync(PatientDto patientDto);
        Task<PatientDto> EditPatientAsync(int id, PatientDto patientDto);
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task<(IEnumerable<PatientDto> Patients, int TotalCount)> GetAllPatientsAsync(int page, int pageSize);
        Task ActivatePatientAsync(int id);
        Task InactivatePatientAsync(int id);
        Task SelectPrimaryClinicAsync(int patientId, Clinic clinic);
        Task SelectPrimaryDoctorAsync(int patientId, int doctorId);
    }
}
