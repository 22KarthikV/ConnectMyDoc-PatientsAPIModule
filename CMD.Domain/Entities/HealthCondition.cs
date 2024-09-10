using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Domain.Entities
{
    public class HealthCondition
    {
        public int HealthConditionId { get; set; }
        public string Condition { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        [ForeignKey(nameof(HealthCondition))]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
