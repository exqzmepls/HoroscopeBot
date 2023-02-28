using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands;

public class HelloCommand : ICommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public HelloCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task ExecuteAsync(Message requestMessage)
    {
        var chat = requestMessage.Chat;
        var chatId = chat.Id;
        var text = $"Hello, {chat.FirstName}!";
        await _telegramBotClient.SendTextMessageAsync(chatId, text);
    }

    public bool IsRequestedByMessage(string? text)
    {
        var trimmedText = text?.Trim();
        var isRequested = trimmedText == "/hello";
        return isRequested;
    }
}