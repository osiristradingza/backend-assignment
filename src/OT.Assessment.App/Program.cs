using OT.Assessment.Consumer;
using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlayerWriter, PlayerWriter>();
builder.Services.AddScoped<IPlayerReader, PlayerReader>();
builder.Services.AddScoped<ICasinoWagerWriter, CasinoWagerWriter>();
builder.Services.AddScoped<ICasinoWagerReader, CasinoWagerReader>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ICasinoEventPublisherService, CasinoEventPublisherService>();
builder.Services.AddScoped<ICasinoWagerService, CasinoWagerService>();
builder.Services.AddScoped<IGameReader, GameReader>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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

app.Run();
