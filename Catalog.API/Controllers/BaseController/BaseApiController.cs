using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.BaseController;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected Guid GetRequestOwner()
    {
        var uidClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        return uidClaim == null ? Guid.Empty : Guid.Parse(uidClaim);
    }

    protected string GenerateIpAddress()
    {
        try
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"]!;
            return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "127.0.0.1";
        }
    }
}