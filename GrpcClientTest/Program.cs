using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using LabProject.Grpc;

class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7132");
        var client = new UserService.UserServiceClient(channel);

        var reply = await client.GetUserAsync(new GetUserRequest { Id = 1 });
        Console.WriteLine($"Name: {reply.Name}, Email: {reply.Email}");
    }
}
