using Microsoft.Extensions.Configuration;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Database.Helper;
using OT.Assessment.Database.Interface;
using OT.Assessment.Manager.UseCases.Accounts.Interfaces;
using OT.Assessment.Manager.UseCases.Accounts.Repository;
using OT.Assessment.Manager.UseCases.Games.Implementation;
using OT.Assessment.Manager.UseCases.Games.Interface;
using OT.Assessment.Manager.UseCases.Wagers.Implementation;
using OT.Assessment.Manager.UseCases.Wagers.Interface;
using OT.Assessment.Messaging.Producer.Interface;
using OT.Assessment.Messaging.Producer.Service;
using RabbitMQ.Client;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Register RabbitMQ connection as a singleton
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" }; // Change if using cloud RabbitMQ
    return factory.CreateConnection();
});

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

// Get the connection string from the configuration
var connectionString = configuration.GetConnectionString("DatabaseConnection");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IDatabaseConnection>(new DatabaseConnection(connectionString));

builder.Services.AddScoped<IWagers, WagersRepository>();
builder.Services.AddScoped<IWagerManager, WagerManager>();
builder.Services.AddScoped<IGames, GamesRepository>();
builder.Services.AddScoped<IGameManager, GameManager>();
builder.Services.AddScoped<IAccounts, AccountsRepository>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IProducerService, ProducerService>();
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

app.Run();
