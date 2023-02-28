using Telegram.Bot.Types;

namespace TelegramBot.Commands;

public interface ICommand
{
    public bool IsRequestedByMessage(string? text);
    public Task ExecuteAsync(Message requestMessage);
}