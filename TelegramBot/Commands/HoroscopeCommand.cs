using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;

namespace TelegramBot.Commands;

public class HoroscopeCommand : ICommand
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IHoroscopeClient _horoscopeClient;

    public HoroscopeCommand(ITelegramBotClient telegramBotClient, IHoroscopeClient horoscopeClient)
    {
        _telegramBotClient = telegramBotClient;
        _horoscopeClient = horoscopeClient;
    }

    public bool IsRequestedByMessage(string? text)
    {
        if (text == null)
            return false;

        var trimmedText = text.Trim();
        var regex = new Regex(@"\/horoscope [0-9]{2}\.[0-9]{2}\.[0-9]{4}");
        var isMatch = regex.IsMatch(trimmedText);
        return isMatch;
    }

    public async Task ExecuteAsync(Message requestMessage)
    {
        var trimmedText = requestMessage.Text!.Trim();
        var regex = new Regex(@"\/horoscope ([0-9]{2}\.[0-9]{2}\.[0-9]{4})");
        var match = regex.Match(trimmedText);
        var birthDateGroup = match.Groups.Values.Skip(1).Single();
        var birthDate = birthDateGroup.Value;

        var zodiacSign = _horoscopeClient.GetZodiacSignOrDefault(birthDate);
        var chatId = requestMessage.Chat.Id;
        if (!zodiacSign.HasValue)
        {
            await _telegramBotClient.SendTextMessageAsync(chatId, $"Impossible to get Zodiac Sign for '{birthDate}'.");
            return;
        }

        var horoscope = _horoscopeClient.GetHoroscope(zodiacSign.Value);
        await _telegramBotClient.SendTextMessageAsync(chatId, horoscope);
    }
}