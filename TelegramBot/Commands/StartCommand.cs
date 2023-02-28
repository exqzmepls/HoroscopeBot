using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands;

public class StartCommand : ICommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public StartCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task ExecuteAsync(Message requestMessage)
    {
        var chat = requestMessage.Chat;
        var chatId = chat.Id;
        const string text = "Welcome! Use /about command to see bot features.";
        await _telegramBotClient.SendTextMessageAsync(chatId, text);
    }

    public bool IsRequestedByMessage(string? text)
    {
        var trimmedText = text?.Trim();
        var isRequested = trimmedText == "/start";
        return isRequested;
    }
}