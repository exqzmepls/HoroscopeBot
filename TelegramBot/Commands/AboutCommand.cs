using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands;

public class AboutCommand : ICommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public AboutCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task ExecuteAsync(Message requestMessage)
    {
        var chatId = requestMessage.Chat.Id;
        var commandsList = new[]
        {
            "/hello - bot tells you hello",
            "/about - displays available commands",
            "/horoscope <birth date> - displays today's horoscope by birth date, for example /horoscope 04.04.2001 (date format should be 'DD.MM.YYYY')"
        };
        var aboutText = $"List of available commands:\n{string.Join('\n', commandsList)}";
        await _telegramBotClient.SendTextMessageAsync(chatId, aboutText);
    }

    public bool IsRequestedByMessage(string? text)
    {
        var trimmedText = text?.Trim();
        var isRequested = trimmedText == "/about";
        return isRequested;
    }
}