using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Services;

public interface ICommandsService
{
    public ICommand GetCommand(Message message);
}