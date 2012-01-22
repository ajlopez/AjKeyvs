using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AjKeyvs.Server.Parser;

namespace AjKeyvs.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            TextReader reader = System.Console.In;
            TextWriter writer = System.Console.Out;

            Session session = new Session(repository, reader, writer);
            session.Process();
        }
    }
}
