using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Email.API.Controllers;


    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register()
        {
            BackgroundJob.Enqueue(() => SendWelcomeMail());

            return Ok();
        }

        public static void SendWelcomeMail()
        {
            Console.WriteLine("Welcome mail has been sent to user");
        }

    }

