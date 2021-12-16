using Assessment.Health.Domain.ApiMedic.Models;
using Assessment.Health.Service.Contract.ApiMedic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assessment.Health.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMedicController : ControllerBase
    {
        private readonly IApiMedicService _apiMediSer;

        public ApiMedicController(IApiMedicService apiMediSer)
        {
            _apiMediSer = apiMediSer;
        }
        // GET: api/<ApiMedicController>
        [HttpGet("Symptoms")]
        public async Task<IActionResult> GetSymptomsAsync()
        {
            return Ok(await _apiMediSer.GetSymptomAsync(null));
        }

        [HttpGet("Issues")]
        public async Task<IActionResult> GetIssuesAsync()
        {
            return Ok(await _apiMediSer.GetIssueAsync(null));
        }

        [HttpGet("Diagnosis/{symptom}/{gender}/{yearOfBirth}")]
        public async Task<IActionResult> GetDiagnosisAsync(string symptom,string gender,int yearOfBirth)
        {
            var symptoms = symptom.Split(",").Select(e=>Convert.ToInt32(e)).ToArray();
            return Ok(await _apiMediSer.GetDiagnosisAsync(symptoms,gender,yearOfBirth));
        }
       
    }
}
