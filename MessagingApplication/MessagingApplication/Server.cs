using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessagingApplication
{
    class Server
    {
        ServerSocket serverSocket;
        List<ClientHandler> clientList = new List<ClientHandler>();

        public Server(ServerSocket serverSock)   
        {
            serverSocket = serverSock;
        }

        public void StartServer()
        {
            try
            {
                while (!serverSocket.isClosed)
                {
                    Console.WriteLine("Waiting");
                    serverSocket.Listen();
                    Socket socket = serverSocket.Accept();
                    clientList.Add(new ClientHandler(socket));

                    //start a new thread for the latest client
                    Thread clientThread = new Thread(new ThreadStart(clientList[clientList.Count-1].HandleClient));
                    clientThread.Start();
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Server Socket Exception");
            }

        }

    }
}
