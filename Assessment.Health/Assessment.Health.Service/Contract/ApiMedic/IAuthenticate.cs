using Assessment.Health.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Contract.ApiMedic
{
    public interface IAuthenticate
    {
        string GetLoginToken();
        ApiMedicSettings Config();
    }
}
