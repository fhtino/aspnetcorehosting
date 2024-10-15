using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace WSClient
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            int counter = 0;

            Console.WriteLine("START");

            await Task.Delay(500);

            string wsUrl = "wss://localhost:7085/ws";

            var wsClient = new ClientWebSocket();

            await wsClient.ConnectAsync(new Uri(wsUrl), CancellationToken.None);

            var receiveTask = Task.Run(async () =>
                {
                    Console.WriteLine("START RECEIVE TASK");

                    var buffer = new byte[1024];

                    while (true)
                    {
                        WebSocketReceiveResult result = await wsClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        string s = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        Console.WriteLine("Receive: " + result.Count + " : " + s);
                    }
                });

            var sendTask = Task.Run(async () =>
                {
                    Console.WriteLine("START SEND TASK");

                    while (true)
                    {
                        var s = $"Hello {counter++} {DateTime.UtcNow.ToString("O")}";
                        Console.WriteLine("Sending..." + s);

                        var buffer = Encoding.UTF8.GetBytes(s);
                        await wsClient.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                        //await Task.Delay(1);
                    }

                });

            await Task.WhenAll(receiveTask, sendTask);



            Console.WriteLine("END");
        }
    }
}
