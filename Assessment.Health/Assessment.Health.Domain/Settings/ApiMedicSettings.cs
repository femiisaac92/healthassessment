using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.Settings
{
    public class ApiMedicSettings
    {
        public string BaseUrl { get; set; }
        public string BaseAuthUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Auth { get; set; }
        public string Symptoms { get; set; }
        public string Issues { get; set; }
        public string Diagnosis { get; set; }
    }

    public class ApiMedicAuth
    {
        public string Token { get; set; }
        public int ValidThrough { get; set; }
    }
}
