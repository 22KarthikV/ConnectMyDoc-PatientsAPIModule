using AutoMapper;
using CMD.Application.DTOs;
using CMD.Application.Interfaces;
using CMD.Domain.Enums;
using CMD.Application.DTOs;
using CMD.Application.Interfaces;
using CMD.Domain.Entities;
using CMD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMD.Infrastructure.Data.Repositories;

namespace CMD.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDto> AddPatientAsync(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            patient.CreatedDate = DateTime.UtcNow;
            patient.LastModifiedDate = DateTime.UtcNow;
            patient.Status = "Active";

            var addedPatient = await _patientRepository.AddAsync(patient);
            return _mapper.Map<PatientDto>(addedPatient);
        }

        public async Task<PatientDto> EditPatientAsync(int id, PatientDto patientDto)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(id);
            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            _mapper.Map(patientDto, existingPatient);
            existingPatient.LastModifiedDate = DateTime.UtcNow;

            var updatedPatient = await _patientRepository.UpdateAsync(existingPatient);
            return _mapper.Map<PatientDto>(updatedPatient);
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<(IEnumerable<PatientDto> Patients, int TotalCount)> GetAllPatientsAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var patients = await _patientRepository.GetAllAsync(skip, pageSize);
            var totalCount = await _patientRepository.GetTotalCountAsync();

            return (_mapper.Map<IEnumerable<PatientDto>>(patients), totalCount);
        }

        public async Task ActivatePatientAsync(int id)
        {
            await _patientRepository.ActivateAsync(id);
        }

        public async Task InactivatePatientAsync(int id)
        {
            await _patientRepository.InactivateAsync(id);
        }

        public async Task SelectPrimaryClinicAsync(int patientId, Clinic clinic)
        {
            await _patientRepository.UpdatePrimaryClinicAsync(patientId, clinic);
        }

        public async Task SelectPrimaryDoctorAsync(int patientId, int doctorId)
        {
            await _patientRepository.UpdatePrimaryDoctorAsync(patientId, doctorId);
        }
    }
}
