using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Services;
using RabbitMQ.Client;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//log errors
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//connect to db
builder.Services.AddDbContext<CasinoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CasinoContext")));

// Register RabbitMQ services
builder.Services.AddSingleton<RabbitMQConnection>(sp => new RabbitMQConnection("localhost"));
builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddSingleton<RabbitMQConsumerService>();
builder.Services.AddScoped<CasinoWagerService>();

builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    return new ConnectionFactory()
    {
        HostName = "localhost", // server name
        UserName = "guest", // username for RabbitMQ
        Password = "guest" // password for RabbitMQ
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
        opts.DocumentTitle = "OT Assessment App";
        opts.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//consume message
using (var scope = app.Services.CreateScope())
{
    var consumerService = scope.ServiceProvider.GetRequiredService<RabbitMQConsumerService>();
    consumerService.StartConsuming();
}

app.Run();
