using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApplication
{
    class ServerSocket
    {
        public Socket underlyingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        public bool isClosed { get; set; }

        public ServerSocket(int port)
        {
            var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            underlyingSocket.Bind(localEndpoint);
            isClosed = false;

        }

        public Socket Accept()
        {
            return underlyingSocket.Accept();
        }

        public void Listen()
        {
            underlyingSocket.Listen(10);
        }

        public void Close()
        {
            underlyingSocket.Close();
            isClosed = true;
        }

        /*
        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        */
    }
}
