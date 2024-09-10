using CMD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public DateTime PreferredStartTime { get; set; }
        public DateTime PreferredEndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public string Status { get; set; }
        public Clinic PreferredClinic { get; set; }
        public long? PrimaryDoctorId { get; set; }
        public PatientAddress Address { get; set; }
        public List<HealthCondition> HealthConditions { get; set; }
    }
}
