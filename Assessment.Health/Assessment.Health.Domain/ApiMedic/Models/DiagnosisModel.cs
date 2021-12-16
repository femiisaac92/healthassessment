using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.ApiMedic.Models
{
    public class DiagnosisModel
    {
        public IssueModel Issue { get; set; }
        public SpecialisationsModel Specialisation { get; set; }
    }

    public class DiagnosisesModel: List<DiagnosisModel>
    {

    }
}
