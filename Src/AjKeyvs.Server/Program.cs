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

            while (true)
            {
                Processor processor = new Processor(repository, new CommandReader(reader, true));

                Console.Write("ajkeyvs> ");

                for (CommandResult result = processor.ProcessCommand(); result != null; result = processor.ProcessCommand())
                {
                    if (result.HasValue)
                    {
                        if (result.Value is String)
                        {
                            Console.Write('"');
                            Console.Write(result.Value);
                            Console.WriteLine('"');
                        }
                        else
                            Console.WriteLine(result.Value.ToString());
                    }
                    else
                        Console.WriteLine("OK");
                }
            }
        }
    }
}
