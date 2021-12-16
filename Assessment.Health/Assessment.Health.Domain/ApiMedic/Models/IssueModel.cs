using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Domain.ApiMedic.Models
{
    public class IssueModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfName { get; set; }
        public string Icd { get; set; }
        public string IcdName { get; set; }
        public string Accuracy { get; set; }
    }
    public class IssuesModel : List<IssueModel>
    {

    }
}
