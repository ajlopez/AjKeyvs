using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AjKeyvs.Server.Parser;

namespace AjKeyvs.Server
{
    public class Processor
    {
        private CommandReader reader;
        private Repository repository;

        public Processor(Repository repository, string text)
            : this(repository, new CommandReader(text))
        {
        }

        public Processor(Repository repository, TextReader reader)
            : this(repository, new CommandReader(reader))
        {
        }

        public Processor(Repository repository, CommandReader reader)
        {
            this.repository = repository;
            this.reader = reader;
        }

        public object Process()
        {
            Command command = this.reader.NextCommand();

            if (command == null)
                return null;

            if (command.Verb == "set")
            {
                string key = command.Key;
                object value = command.Parameters[0];

                this.repository.SetValue(key, value);
                return null;
            }

            if (command.Verb == "get")
            {
                string key = command.Key;

                return this.repository.GetValue(key);
            }

            throw new InvalidDataException();
        }
    }
}
