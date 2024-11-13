using SmsRateLimiterService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read rate limits from configuration
int perNumberLimit = builder.Configuration.GetValue<int>("RateLimits:MessagesPerSecondPerNumber");
int accountLimit = builder.Configuration.GetValue<int>("RateLimits:MessagesPerSecondAccount");

// Register the RateLimiterService as a singleton, passing in the rate limits
builder.Services.AddSingleton(new RateLimiterService(perNumberLimit, accountLimit));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
