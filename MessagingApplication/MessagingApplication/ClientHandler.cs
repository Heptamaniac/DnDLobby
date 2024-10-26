using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApplication
{
    class ClientHandler
    {
        Socket clientSocket;

        public ClientHandler(Socket socket)
        {
            clientSocket = socket;
        }

        public void HandleClient()
        {

            while (clientSocket.Connected)
            {
                if (clientSocket.Available > 0)
                {
                    byte[] dataBuffer = new byte[1000];
                    int recievedLength = clientSocket.Receive(dataBuffer, dataBuffer.Length, SocketFlags.None);
                    Console.WriteLine(Encoding.ASCII.GetString(dataBuffer, 0, recievedLength));

                    SendConfirmation();
                }
            }
        }

        public void SendConfirmation()
        {
            byte[] sendData = new byte[1];
            sendData[0] = (byte)SharedClasses.Constants.Headers.Confirmation;
            clientSocket.Send(sendData);
        }
    }
}
