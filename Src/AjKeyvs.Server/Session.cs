namespace AjKeyvs.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using AjKeyvs.Server.Parser;

    public class Session
    {
        private TextReader reader;
        private TextWriter writer;
        private Repository repository;
        private bool isconsole;

        public Session(Repository repository, TextReader reader, TextWriter writer)
        {
            this.repository = repository;
            this.reader = reader;
            this.writer = writer;
            this.isconsole = this.reader == System.Console.In;
        }

        public void Process()
        {
            Processor processor = new Processor(this.repository, new CommandReader(this.reader, false));

            if (this.isconsole)
                this.writer.Write("ajkeyvs> ");

            for (CommandResult result = processor.ProcessCommand(); result != null; result = processor.ProcessCommand())
            {
                if (result.HasValue)
                {
                    if (result.Value is String)
                    {
                        this.writer.Write('"');
                        this.writer.Write(result.Value);
                        this.writer.WriteLine('"');
                    }
                    else if (result.Value == null)
                        this.writer.WriteLine("null");
                    else
                        this.writer.WriteLine(result.Value.ToString());
                }
                else
                    this.writer.WriteLine("OK");

                if (this.isconsole)
                    this.writer.Write("ajkeyvs> ");
            }
        }
    }
}
