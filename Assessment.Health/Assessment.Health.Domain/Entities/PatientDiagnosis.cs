using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.Entities
{
    public class PatientDiagnosis : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int DiagnosisId { get; set; }
        public string DiagnosisName { get; set; }
       // [JsonIgnore]
       // public virtual Appointment Appointment { get; set; }
    }
}
