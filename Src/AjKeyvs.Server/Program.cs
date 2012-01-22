using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AjKeyvs.Server.Parser;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace AjKeyvs.Server
{
    class Program
    {
        private static Repository repository;

        static void Main(string[] args)
        {
            repository = new Repository();

            TextReader reader = System.Console.In;
            TextWriter writer = System.Console.Out;


            if (args != null && args.Length > 1)
            {
                IPAddress address = IPAddress.Parse(args[0]);
                int port = Int32.Parse(args[1]);
                TcpListener listener = new TcpListener(address, port);
                ParameterizedThreadStart start = new ParameterizedThreadStart(Listen);
                Thread thread = new Thread(start);
                thread.Start(listener);
            }

            Session session = new Session(repository, reader, writer);
            session.Process();
        }

        static void Listen(object parameter)
        {
            TcpListener listener = (TcpListener) parameter;
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                TextReader reader = new StreamReader(stream);
                TextWriter writer = new StreamWriter(stream);
                Session session = new Session(repository, reader, writer);
                session.Process();
            }
        }
    }
}
