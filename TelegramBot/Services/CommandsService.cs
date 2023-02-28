using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Services;

public class CommandsService : ICommandsService
{
    private readonly ICommand _unknownCommand;
    private readonly IReadOnlyCollection<ICommand> _commands;

    public CommandsService(ITelegramBotClient telegramBotClient)
    {
        _unknownCommand = new UnknownCommand(telegramBotClient);

        _commands = new ICommand[]
        {
            new StartCommand(telegramBotClient),
            new AboutCommand(telegramBotClient),
            new HelloCommand(telegramBotClient),
            
        };
    }

    public ICommand GetCommand(Message message)
    {
        var messageText = message.Text;
        var command = _commands.SingleOrDefault(c => c.IsRequestedByMessage(messageText));
        return command ?? _unknownCommand;
    }
}