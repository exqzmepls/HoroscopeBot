namespace TelegramBot.Clients;

public class HoroscopeClient : IHoroscopeClient
{
    public ZodiacSign? GetZodiacSignOrDefault(string birthDate)
    {
        throw new NotImplementedException();
    }

    public string GetHoroscope(ZodiacSign zodiacSign)
    {
        throw new NotImplementedException();
    }
}