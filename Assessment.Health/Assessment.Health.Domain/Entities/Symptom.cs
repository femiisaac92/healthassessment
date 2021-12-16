using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.Entities
{
    public class Symptom : BaseEntity
    {
        
        [Required]
        public string Name { get; set; }        
        public int SymptomId { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
