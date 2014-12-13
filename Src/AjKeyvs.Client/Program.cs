namespace AjKeyvs.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            string hostname = args[0];
            int port = Int32.Parse(args[1]);
            TcpClient client = new TcpClient(hostname, port);

            Stream stream = client.GetStream();
            TextReader reader = new StreamReader(stream);
            TextWriter writer = new StreamWriter(stream);

            while (true)
            {
                Console.Write("ajkeyvs-client> ");
                string command = Console.ReadLine();

                if (command == "exit")
                    break;

                writer.WriteLine(command);
                writer.Flush();
                string result = reader.ReadLine();
                Console.WriteLine(result);
            }

            client.Close();
        }
    }
}
