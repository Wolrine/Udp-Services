using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServerApp
{
    public class ClientCon
    {
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in host)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void ConnectServer(string host, int port)
        {
            UdpClient client = new();
            IPAddress address = IPAddress.Parse(host);
            IPEndPoint remoteEndpoint = new(address, port);
            string message;
            int count = 0;
            bool done = false;
            Console.Title = "UDP Server";
            Console.WriteLine(new string('*', 40));
            try
            {
                Console.WriteLine(new string('*', 40));
                client.Connect(remoteEndpoint);
                while (!done)
                {
                    message = $"Message {++count:D2}";
                    byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                    client.Send(sendBytes, sendBytes.Length);
                    Console.WriteLine($"Sent: {message}");
                    Thread.Sleep(2000);
                    if (count == 10)
                    {
                        done = true;
                        Console.WriteLine($"Done.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                client.Close();
            }
        }
    }
}
