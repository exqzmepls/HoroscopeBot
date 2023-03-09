using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;
using TelegramBot.Commands;

namespace TelegramBot.Services;

public class CommandsService : ICommandsService
{
    private readonly ICommand _unknownCommand;
    private readonly IReadOnlyCollection<ICommand> _commands;

    public CommandsService(ITelegramBotClient telegramBotClient, IHoroscopeClient horoscopeClient)
    {
        _unknownCommand = new UnknownCommand(telegramBotClient);

        _commands = new ICommand[]
        {
            new StartCommand(telegramBotClient),
            new AboutCommand(telegramBotClient),
            new HelloCommand(telegramBotClient),
            new HoroscopeCommand(telegramBotClient, horoscopeClient)
        };
    }

    public ICommand GetCommand(Message message)
    {
        var messageText = message.Text;
        var command = _commands.SingleOrDefault(c => c.IsRequestedByMessage(messageText));
        return command ?? _unknownCommand;
    }
}