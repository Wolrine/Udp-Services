using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServerApp
{
    public class ServerCon
    {
        public ServerCon() { }
        public ServerCon(int port) => ListenPort = port;
        public ServerCon(string host, int port)
        {
            Host = host;
            ListenPort = port;
        }
        private string Host { get; set; } = string.Empty;
        private int ListenPort { get; set; }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var ip in host)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    if (Host.Equals(String.Empty))
                        Host = ip.ToString();
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void StartListener()
        {
            string message;
            UdpClient listener = new(ListenPort);
            IPAddress address = IPAddress.Parse(Host);
            IPEndPoint remoteEndpoint = new(address, ListenPort);
            Console.Title = "UDP Server";
            Console.WriteLine(new string('*', 40));
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref remoteEndpoint);
                    message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine($"Received broadcast from {remoteEndpoint}:{message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
