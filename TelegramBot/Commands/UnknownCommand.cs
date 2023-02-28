using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands;

public class UnknownCommand : ICommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public UnknownCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task ExecuteAsync(Message requestMessage)
    {
        var chatId = requestMessage.Chat.Id;
        await _telegramBotClient.SendTextMessageAsync(chatId, "unknown command");
    }

    public bool IsRequestedByMessage(string? text)
    {
        return false;
    }
}