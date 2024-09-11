using CMD.Domain.Enums;
using CMD.Infrastructure.Data.Repositories;
using CMD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMD.Infrastructure.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            
            var existingPatient = await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.HealthConditions)
                .FirstOrDefaultAsync(p => p.Id == patient.Id);

            if (existingPatient != null)
            {
                _context.Entry(existingPatient).CurrentValues.SetValues(patient);

                // Update Address if necessary
                if (patient.Address != null)
                {
                    var existingAddress = existingPatient.Address;
                    if (existingAddress != null)
                    {
                        _context.Entry(existingAddress).CurrentValues.SetValues(patient.Address);
                    }
                    else
                    {
                        existingPatient.Address = patient.Address;
                        _context.Entry(patient.Address).State = EntityState.Added;
                    }
                }

                // Update HealthConditions
                foreach (var condition in patient.HealthConditions)
                {
                    var existingCondition = existingPatient.HealthConditions
                        .FirstOrDefault(hc => hc.HealthConditionId == condition.HealthConditionId);

                    if (existingCondition != null)
                    {
                        _context.Entry(existingCondition).CurrentValues.SetValues(condition);
                    }
                    else
                    {
                        existingPatient.HealthConditions.Add(condition);
                        _context.Entry(condition).State = EntityState.Added;
                    }
                }

                await _context.SaveChangesAsync();
            }

            return patient;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.HealthConditions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(int skip, int take)
        {
            return await _context.Patients
                .Include(p => p.Address)
                .Include(p => p.HealthConditions)
                .OrderBy(p => p.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Patients.CountAsync();
        }

        public async Task ActivateAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.Status = "Active";
                patient.LastModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task InactivateAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.Status = "Inactive";
                patient.LastModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePrimaryClinicAsync(int patientId, Clinic clinic)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                patient.PreferredClinic = clinic;
                patient.LastModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePrimaryDoctorAsync(int patientId, int doctorId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                patient.PrimaryDoctorId = doctorId;
                patient.LastModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
