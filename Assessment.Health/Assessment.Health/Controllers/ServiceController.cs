using Assessment.Health.Service.Features.CustomerFeatures.Commands;
using Assessment.Health.Service.Features.CustomerFeatures.Queries;
using Assessment.Health.Service.Features.ServiceFeatures.Commands;
using Assessment.Health.Service.Features.ServiceFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Assessment.Health.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPatientDiagnoses()));
        }
        [HttpGet]
        [Route("appointment")]
        public async Task<IActionResult> GetAllAppointment()
        {
            return Ok(await Mediator.Send(new GetAllPatientAppointments()));
        }
    }
}
