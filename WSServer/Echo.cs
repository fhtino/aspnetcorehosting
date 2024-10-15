using System.Net.WebSockets;
using System.Text;

namespace WSServer
{
    public class Echo
    {
        private static int Counter = 0;

        public static async Task Run(WebSocket webSocket)
        {
            Interlocked.Increment(ref Counter);

            try
            {
                /*
                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);

                    receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
                */


                var buffer = new byte[1024 * 4];

                while (true)
                {
                    var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    
                      string s = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                    Console.WriteLine(receiveResult.EndOfMessage + " : " + s);

                    if (receiveResult.CloseStatus.HasValue)
                    {
                        break;
                    }

                    await Task.Delay(2000);

                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                }





                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
