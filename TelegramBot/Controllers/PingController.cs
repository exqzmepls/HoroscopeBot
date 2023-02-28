using Microsoft.AspNetCore.Mvc;

namespace TelegramBot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping()
    {
        return Ok("ok");
    }
}