using SmsRateLimiterService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(new RateLimiterService(
    builder.Configuration.GetValue<int>("RateLimits:MessagesPerSecondPerNumber"),
    builder.Configuration.GetValue<int>("RateLimits:MessagesPerSecondAccount")
));

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
