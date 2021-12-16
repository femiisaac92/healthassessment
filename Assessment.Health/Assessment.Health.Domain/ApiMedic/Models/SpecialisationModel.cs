using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.ApiMedic.Models
{
    public class SpecialisationModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SpecialistID { get; set; }
    }

    public class SpecialisationsModel : List<SpecialisationModel>
    {

    }
}
