using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Controllers;

[Route("api/update")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly ICommandsService _commandsService;

    public MessagesController(ICommandsService commandsService)
    {
        _commandsService = commandsService;
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody, Required] Update update)
    {
        var message = update.Message;

        if (message == null)
            return Ok();

        var command = _commandsService.GetCommand(message);
        await command.ExecuteAsync(message);

        return Ok();
    }
}