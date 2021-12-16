using Assessment.Health.Domain.ApiMedic.Models;
using Assessment.Health.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Contract.ApiMedic
{
    public interface IApiMedicService
    {
        Task<Response<SymptomsModel>> GetSymptomAsync(int[] symptoms);
        Task<Response<IssuesModel>> GetIssueAsync(int[] issues);
        Task<Response<DiagnosisesModel>> GetDiagnosisAsync(int[] symptoms,string gender,int YearOfBirth);
    }
}
