using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NATS.Client.Internals;
using OT.Assessment.App.Services;

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

// Create the host and register services including RabbitMQConnection
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register RabbitMQConnection as a singleton
        services.AddSingleton<RabbitMQConnection>(sp => new RabbitMQConnection("localhost"));
    })
    .Build();

var rabbitMQConnection = host.Services.GetRequiredService<RabbitMQConnection>();
using var httpClient = new HttpClient(handler);

var scenario = Scenario.Create("hello_world_scenario", async context =>
{
    var connection = rabbitMQConnection.GetConnection();
    if (connection == null || !connection.IsOpen)
    {
        return Response.Fail("RabbitMQ connection is not available", "", "", 0);
    }

    var bg = new BogusGenerator();
    var total = bg.Generate();
    var body = JsonSerializer.Serialize(total[(int)context.InvocationNumber]);
    Console.WriteLine($"Request Body: {body}"); // Log the request body

    var request = Http.CreateRequest("POST", "https://localhost:7120/api/player/casinowager")
        .WithHeader("Accept", "application/json")
        .WithBody(new StringContent(body, Encoding.UTF8, "application/json"));

    // Send the request and get the response
    var response = await Http.Send(httpClient, request);

    // Check if the response is successful
    if (response.StatusCode == "OK")
    {
        Console.WriteLine($"Request succeeded: {body}");
        return Response.Ok();
    }
    else
    {
        Console.WriteLine($"Request failed: {response.StatusCode}, Message: {response.Message}");
        return Response.Fail(body, response.StatusCode, response.Message, response.SizeBytes);
    }
})
.WithoutWarmUp()
//.WithLoadSimulations(Simulation.IterationsForInject(rate: 10,
//    interval: TimeSpan.FromSeconds(5),
//    iterations: TimeSpan.FromSeconds(5)));

.WithLoadSimulations(
        Simulation.IterationsForInject(rate: 500,
            interval: TimeSpan.FromSeconds(2),
            iterations: 7000)
    );


NBomberRunner
    .RegisterScenarios(scenario)
    .WithWorkerPlugins(new HttpMetricsPlugin(new[] { HttpVersion.Version1 }))
    .WithoutReports()
    .Run();