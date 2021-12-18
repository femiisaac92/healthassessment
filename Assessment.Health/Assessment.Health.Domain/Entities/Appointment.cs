using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.Entities
{
   public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }

        //VITALS
        public int Temp { get; set; }
        public int Bpressure { get; set; }
        public int Prate { get; set; }
        public int Rrate { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        public int Symptoms { get; set; }
        public string Specialisation { get; set; }
        public string SymptomName { get; set; }
        public virtual Patient Patient { get; set; }
        [JsonIgnore]
        public virtual ICollection<PatientDiagnosis> patientDiagnoses { get; set; }

    }
}
