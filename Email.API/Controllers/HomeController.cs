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

        [HttpGet("addrecurringjob")]
        public IActionResult AddRecurringJob()
        {
            RecurringJob.AddOrUpdate("Pending Deliveries Job", () => CheckPendingDeliveries(), Cron.Minutely);

            return Ok();
        }

        public static void SendWelcomeMail()
        {
            Console.WriteLine("Welcome mail has been sent to user");
        }

        public static void SendReminder()
        {
            bool completed = new Random().Next() % 2 == 0;

            if (completed)
            {
                Console.WriteLine("The user has already completed the order for cart item xxxx");
            }
            else
            {
                Console.WriteLine("A reminder has been sent to user for cart item xxxx");
            }
        }

        public static void CheckPendingDeliveries()
        {
            int number = new Random().Next(1, 10);

            Console.WriteLine($"There are {number} pending deliveries");
        }
    }

