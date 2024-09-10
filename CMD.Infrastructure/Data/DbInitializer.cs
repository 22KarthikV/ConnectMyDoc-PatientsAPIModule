// CMD.Patient.Infrastructure/Data/DbInitializer.cs

using CMD.Domain.Entities;
using CMD.Domain.Enums;
using CMD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMD.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any patients.
                if (context.Patients.Any())
                {
                    return;   // DB has been seeded
                }

                var patients = new List<Domain.Entities.Patient>
                {
                    new Domain.Entities.Patient
                    {
                        Name = "John Doe",
                        Email = "john.doe@example.com",
                        Phone = "1234567890",
                        Age = 30,
                        Dob = new DateTime(1993, 1, 1),
                        Gender = "Male",
                        PreferredStartTime = new DateTime(2023, 1, 1, 9, 0, 0),
                        PreferredEndTime = new DateTime(2023, 1, 1, 17, 0, 0),
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = 1, // Assuming 1 is the ID of the system or admin user
                        LastModifiedDate = DateTime.UtcNow,
                        LastModifiedBy = 1,
                        Status = "Active",
                        PreferredClinic = Clinic.GeneralPractice,
                        PrimaryDoctorId = 1, // Assuming 1 is a valid doctor ID
                        Address = new PatientAddress
                        {
                            StreetAddress = "123 Main St",
                            City = "Anytown",
                            State = "State",
                            Country = "Country",
                            ZipCode = 12345
                        },
                        HealthConditions = new List<HealthCondition>
                        {
                            new HealthCondition
                            {
                                Condition = "Hypertension",
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = 1,
                                LastModifiedDate = DateTime.UtcNow,
                                LastModifiedBy = 1
                            }
                        }
                    },
                    new Domain.Entities.Patient
                    {
                        Name = "Jane Smith",
                        Email = "jane.smith@example.com",
                        Phone = "0987654321",
                        Age = 25,
                        Dob = new DateTime(1998, 5, 15),
                        Gender = "Female",
                        PreferredStartTime = new DateTime(2023, 1, 1, 10, 0, 0),
                        PreferredEndTime = new DateTime(2023, 1, 1, 18, 0, 0),
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = 1,
                        LastModifiedDate = DateTime.UtcNow,
                        LastModifiedBy = 1,
                        Status = "Active",
                        PreferredClinic = Clinic.Cardiology,
                        PrimaryDoctorId = 2, // Assuming 2 is a valid doctor ID
                        Address = new PatientAddress
                        {
                            StreetAddress = "456 Elm St",
                            City = "Othertown",
                            State = "State",
                            Country = "Country",
                            ZipCode = 67890
                        },
                        HealthConditions = new List<HealthCondition>
                        {
                            new HealthCondition
                            {
                                Condition = "Asthma",
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = 1,
                                LastModifiedDate = DateTime.UtcNow,
                                LastModifiedBy = 1
                            }
                        }
                    }
                };

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}