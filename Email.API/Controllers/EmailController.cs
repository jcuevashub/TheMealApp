using Email.Application.Interfaces.Services;
using Email.Core.Dtos;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Email.API.Controllers;


[ApiController]
[Route("/v1/email")]
public class EmailController : ControllerBase
{

    [HttpPost("send-email")]
    public IActionResult Register([FromBody] EmailMessage emailMessage)
    {
        BackgroundJob.Enqueue<ISendEmailJob>(job => job.ExecuteAsync(emailMessage));
        return Ok();
    }


    public static void SendWelcomeMail()
    {
        Console.WriteLine("Welcome mail has been sent to user");
    }

}

