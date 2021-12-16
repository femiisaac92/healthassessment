using Assessment.Health.Service.Contract.ApiMedic;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using Assessment.Health.Domain.Settings;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json;
namespace Assessment.Health.Service.Implementation.ApiMedic
{
    public class Authenticate : IAuthenticate
    {
        IConfiguration _config { get; }
        public ILogger<Authenticate> _logger { get; }

        public Authenticate(IConfiguration config, ILogger<Authenticate> logger)
        {
            _config = config;
            _logger = logger;
        }
        public ApiMedicSettings Config()
        {
           var settings = _config.GetSection("ApiMedicSettings");
            return settings.Get<ApiMedicSettings>();
        }

        public string GetLoginToken()
        {
            string token = string.Empty;
            var config = Config();
            string uri = config.BaseAuthUrl + config.Auth;
            string api_key = config.Username;
            string secret_key = config.Password;
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret_key);
            string computedHashString = "";
            using (HMACMD5 hmac = new HMACMD5(secretBytes))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(uri);
                byte[] computedHash = hmac.ComputeHash(dataBytes);
                computedHashString = Convert.ToBase64String(computedHash);
            }

            using (WebClient client = new WebClient())
            {
                client.Headers["Authorization"] = string.Concat("Bearer ", api_key, ":", computedHashString);
                try
                {
                    string responseArray = client.UploadString(uri, "POST", "");
                    // Deserialize token string
                    var serializer = new JsonSerializer();
                    var _data = JsonConvert.DeserializeObject<ApiMedicAuth>(responseArray);
                    token = _data.Token;
                }
                catch (Exception e)
                {
                    _logger.LogError("Error During ApiMedic Auth",e);
                }
            }
            return token;
        }
    }
}
