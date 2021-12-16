using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.ApiMedic.Models
{
    public class SymptomModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class SymptomsModel : List<SymptomModel>
    {

    }
}
