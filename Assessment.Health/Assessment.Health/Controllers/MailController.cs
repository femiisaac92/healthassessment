﻿using Assessment.Health.Domain.Settings;
using Assessment.Health.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assessment.Health.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Mail")]
    [ApiVersion("1.0")]
    public class MailController : ControllerBase
    {
        private readonly IEmailService mailService;
        public MailController(IEmailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }

    }
}