using Assessment.Health.Domain.ApiMedic.Models;
using Assessment.Health.Domain.Common;
using Assessment.Health.Service.Contract.ApiMedic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Implementation.ApiMedic
{
    public class ApiMedicService : Authenticate, IApiMedicService
    {
        IConfiguration _config { get; }
        public ILogger<ApiMedicService> _logger { get; }

        public ApiMedicService(IConfiguration config, ILogger<ApiMedicService> logger):base(config,logger)
        {
            _config = config;
            _logger = logger;
        }
        public Task<Response<SymptomsModel>> GetSymptomAsync(int[] symptoms = null)
        {
            Response<SymptomsModel> response = new Response<SymptomsModel>();
            //Authente 
            string authToken = GetLoginToken();
            if (!string.IsNullOrEmpty(authToken))
            {
                var config = Config();
                var uri = config.BaseUrl + string.Format(config.Symptoms, authToken, "en-gb");
                if (symptoms!=null)
                    uri += "&symptoms=" + JsonConvert.SerializeObject(symptoms);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                using (HttpWebResponse _response = (HttpWebResponse)request.GetResponseAsync().Result)
                {
                    try
                    {
                        using (Stream responseStream = _response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            var jsonReader = new JsonTextReader(reader);
                            var serializer = new JsonSerializer();
                            var _data = serializer.Deserialize<SymptomsModel>(jsonReader);
                            response.Succeeded = true;
                            response.Message = "Ok";
                            response.Data = _data;
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error During ApiMedic Symptoms", e);
                        response.Succeeded = false;
                        response.Message = e.Message;                       
                    }
                }
            }
            return Task.FromResult(response);
        }

        public Task<Response<IssuesModel>> GetIssueAsync(int[] issues = null)
        {
            Response<IssuesModel> response = new Response<IssuesModel>();
            //Authente 
            string authToken = GetLoginToken();
            if (!string.IsNullOrEmpty(authToken))
            {
                var config = Config();
                var uri = config.BaseUrl + string.Format(config.Symptoms, authToken, "en-gb");
                if (issues != null)
                    uri += "&issues=" + JsonConvert.SerializeObject(issues);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                using (HttpWebResponse _response = (HttpWebResponse)request.GetResponseAsync().Result)
                {
                    try
                    {
                        using (Stream responseStream = _response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            var jsonReader = new JsonTextReader(reader);
                            var serializer = new JsonSerializer();
                            var _data = serializer.Deserialize<IssuesModel>(jsonReader);
                            response.Succeeded = true;
                            response.Message = "Ok";
                            response.Data = _data;
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error During ApiMedicService GetIssueAsync", e);
                        response.Succeeded = false;
                        response.Message = e.Message;
                    }
                }
            }
            return Task.FromResult(response);
        }

        public Task<Response<DiagnosisesModel>> GetDiagnosisAsync(int[] symptoms, string gender, int YearOfBirth)
        {
            Response<DiagnosisesModel> response = new Response<DiagnosisesModel>();
            //Authente 
            string authToken = GetLoginToken();
            if (!string.IsNullOrEmpty(authToken))
            {
                var config = Config();
                var uri = config.BaseUrl + string.Format(config.Diagnosis, authToken, "en-gb", JsonConvert.SerializeObject(symptoms), gender,YearOfBirth);
               
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                using (HttpWebResponse _response = (HttpWebResponse)request.GetResponseAsync().Result)
                {
                    try
                    {
                        using (Stream responseStream = _response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            var jsonReader = new JsonTextReader(reader);
                            var serializer = new JsonSerializer();
                            var _data = serializer.Deserialize<DiagnosisesModel>(jsonReader);
                            response.Succeeded = true;
                            response.Message = "Ok";
                            response.Data = _data;
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error During ApiMedicService GetDiagnosisAsync", e);
                        response.Succeeded = false;
                        response.Message = e.Message;
                    }
                }
            }
            return Task.FromResult(response);
        }
    }
}
