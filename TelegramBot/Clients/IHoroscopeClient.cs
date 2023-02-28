namespace TelegramBot.Clients;

public interface IHoroscopeClient
{
    public ZodiacSign? GetZodiacSignOrDefault(string birthDate);

    public string GetHoroscope(ZodiacSign zodiacSign);
}