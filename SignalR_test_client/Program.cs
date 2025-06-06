using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7108/reviewHub")
    .WithAutomaticReconnect()
    .Build();

connection.On<string>("ReceiveReviewNotification", (message) =>
{
    Console.WriteLine($"Received message: {message}");
});

await connection.StartAsync();
Console.WriteLine("Connected to SignalR Hub. Listening for messages...");

await Task.Delay(-1);
