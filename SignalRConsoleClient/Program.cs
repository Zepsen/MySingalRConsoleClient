using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRConsoleClient
{
    class Program
    {
        private static HubConnection connection;

        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Run!");
            await ConnectionAsync();

            Console.WriteLine("Push 'a' to exit!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            while (true)
            {
                var k = Console.ReadKey();
                if (k.Key == ConsoleKey.A)
                {
                    Console.WriteLine("Break!");
                    break;
                }

                await connection.InvokeAsync("Status");
            }
            

            Console.ReadLine();
        }

        private static async Task ConnectionAsync()
        {
            var token =
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdEB0ZXN0LmNvbSIsIlVzZXJJZCI6IjEiLCJEZXZpY2VJZCI6Il9FSUZZS1I0U0RZSk5LRlZHIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsIm5iZiI6MTU0NjAxMjY3NSwiZXhwIjoxNTQ2MDk5MDc1LCJpc3MiOiJSYW5kZXV2dSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA5MSJ9.MtzRiLXmAEFfhWX90zE6tlDw6ylYTlZBXdjHfD_hbuU";
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:54709/app", (opt) =>
                    {
                        opt.AccessTokenProvider = () => Task.FromResult(token);
                    } )
                .Build();

            await connection.StartAsync();
            Console.WriteLine("Starting...");
        }

        private static void Responses()
        {
            connection.On<string>("Status", (val) => { Console.WriteLine($"Status: {val}"); });
        }
    }
}
