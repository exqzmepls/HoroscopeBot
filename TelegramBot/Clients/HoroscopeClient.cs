using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TelegramBot.Clients;

public class HoroscopeClient : IHoroscopeClient
{
    private const int Year = 2000;
    public readonly Dictionary<ZodiacSign, DateOnly[]> ZodiacDateTimesMap = new()
    {
        { ZodiacSign.Aquarius, new[] { new DateOnly(Year, 1, 20), new DateOnly(Year, 2, 18) } },
        { ZodiacSign.Pisces, new[] { new DateOnly(Year, 2, 19), new DateOnly(Year, 3, 20) } },
        { ZodiacSign.Aries, new[] { new DateOnly(Year, 3, 21), new DateOnly(Year, 4, 19) } },
        { ZodiacSign.Taurus, new[] { new DateOnly(Year, 4, 20), new DateOnly(Year, 5, 20) } },
        { ZodiacSign.Gemini, new[] { new DateOnly(Year, 5, 21), new DateOnly(Year, 6, 20) } },
        { ZodiacSign.Cancer, new[] { new DateOnly(Year, 6, 21), new DateOnly(Year, 7, 22) } },
        { ZodiacSign.Leo, new[] { new DateOnly(Year, 7, 23), new DateOnly(Year, 8, 22) } },
        { ZodiacSign.Virgo, new[] { new DateOnly(Year, 8, 23), new DateOnly(Year, 9, 22) } },
        { ZodiacSign.Libra, new[] { new DateOnly(Year, 9, 23), new DateOnly(Year, 10, 22) } },
        { ZodiacSign.Scorpio, new[] { new DateOnly(Year, 10, 23), new DateOnly(Year, 11, 21) } },
        { ZodiacSign.Sagittarius, new[] { new DateOnly(Year, 11, 22), new DateOnly(Year, 12, 21) } },
        { ZodiacSign.Capricorn, new[] { new DateOnly(Year, 12, 22), new DateOnly(Year, 1, 19) } }
    };

    public readonly Dictionary<ZodiacSign, string> ZodiacTextMap = new()
    {
        { ZodiacSign.Aquarius, "Водолей" },
        { ZodiacSign.Pisces, "Рыбы" },
        { ZodiacSign.Aries, "Овен" },
        { ZodiacSign.Taurus, "Телец"},
        { ZodiacSign.Gemini, "Близнецы"},
        { ZodiacSign.Cancer, "Рак"},
        { ZodiacSign.Leo, "Лев"},
        { ZodiacSign.Virgo, "Дева"},
        { ZodiacSign.Libra, "Весы"},
        { ZodiacSign.Scorpio, "Скорпион"},
        { ZodiacSign.Sagittarius, "Стрелец"},
        { ZodiacSign.Capricorn, "Козерог"},
    };

    public ZodiacSign? GetZodiacSignOrDefault(string birthDate)
    {
        ZodiacSign? zodiacSign = null;
        if (!DateOnly.TryParseExact(birthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateOnly dateTimeBirth))
            return null;

        var dateTimeBirthWithDefYear = new DateOnly(2000, dateTimeBirth.Month, dateTimeBirth.Day);

        foreach (var zodiac in ZodiacDateTimesMap)
            if (dateTimeBirthWithDefYear >= zodiac.Value[0] && dateTimeBirthWithDefYear <= zodiac.Value[1])
                return zodiac.Key;

        return null;
    }

    public string GetHoroscope(ZodiacSign zodiacSign)
    {
        string directory = Directory.GetCurrentDirectory() + @"\Resources";
        string[] files = Directory.GetFiles(directory);



        var first = GetLinesOfFile(files[0]);
        var second = GetLinesOfFile(files[1]);
        var secondAdd = GetLinesOfFile(files[2]);
        var third = GetLinesOfFile(files[3]);

        string?[] GetLinesOfFile(string file)
        {
            List<string?> lines = new(capacity: 30);
            using (StreamReader reader = new StreamReader(file))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line);

            }

            return lines.ToArray();
        }

        Random r = new Random();
        var randomFirst = r.Next(0, first.Length - 1);
        var randomSecond = r.Next(0, second.Length - 1);
        var randomSecondAdd = r.Next(0, secondAdd.Length - 1);
        var randomThird = r.Next(0, third.Length - 1);

        string topic = ZodiacTextMap[zodiacSign] + '\n';
        string mainBody = first[randomFirst] + second[randomSecond] + secondAdd[randomSecondAdd] + third[randomThird];


        return topic + mainBody;
    }
}