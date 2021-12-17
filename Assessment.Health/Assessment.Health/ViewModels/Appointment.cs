using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Health.ViewModels
{
    public class Appointment
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
        public int[] DiagnosisIds { get; set; }
        public string[] diagnosisname { get; set; }

    }
}
