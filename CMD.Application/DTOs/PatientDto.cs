using CMD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Application.DTOs
{
    public class PatientDto
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
        public string Status { get; set; }
        public Clinic PreferredClinic { get; set; }
        public int? PrimaryDoctorId { get; set; }
        public PatientAddressDto Address { get; set; }
        public List<HealthConditionDto> HealthConditions { get; set; }
    }
}
