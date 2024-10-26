using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Client
    {
        Socket client;

        string ServerIPString = "127.0.0.1";
        int ServerPort = 11000;
        IPEndPoint serverEP;

        public Client()
        {
            var serverIP = IPAddress.Parse(SharedClasses.Constants.ServerIpAddress);
            client = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverEP = new IPEndPoint(serverIP, ServerPort);
        }

        public bool Connect()
        {
            try
            {
                Console.WriteLine("Connecting");

                client.Connect(serverEP);
                if (SendMessage("connect"))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(SocketException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Client Socket Exception");
            }

            return false;
        }

        public bool SendMessage(string message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);

            Console.WriteLine("Sending");
            //send message
            client.Send(msg);

            //recieve response
            byte[] bytes = new byte[1000];
            client.Receive(bytes);
            if (bytes[0] == (byte)SharedClasses.Constants.Headers.Confirmation)
            {
                Console.WriteLine("Message Recieved by server");
                return true;
            }

            return false;
        }

    }
}
