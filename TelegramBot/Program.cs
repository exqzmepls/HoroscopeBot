using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot;
using TelegramBot.Clients;
using TelegramBot.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var telegramSection = configuration.GetSection("Telegram");
var tokenSection = telegramSection.GetSection("Token");
var telegramBotClient = new TelegramBotClient(tokenSection.Value);
var webhookSection = telegramSection.GetSection("WebhookUrl");
var webhookUrl = webhookSection.Value + "api/update";
await telegramBotClient.SetWebhookAsync(webhookUrl);

// Add services to the container.

builder.Services.AddSingleton<IHoroscopeClient, HoroscopeClient>();
builder.Services.AddSingleton<ITelegramBotClient>(telegramBotClient);

builder.Services.AddScoped<ICommandsService, CommandsService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    var settings = options.SerializerSettings;
    settings.Formatting = Formatting.Indented;
    settings.ContractResolver = new DefaultContractResolver();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();