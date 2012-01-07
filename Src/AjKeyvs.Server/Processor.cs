using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AjKeyvs.Server.Parser;
using AjKeyvs.Server.Commands;

namespace AjKeyvs.Server
{
    public class Processor
    {
        private static CommandResult ok = new CommandResult();

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

        public CommandResult ProcessCommand()
        {
            CommandInfo command = this.reader.NextCommand();

            if (command == null)
                return null;

            if (command.Verb == "set")
            {
                return ((new SetValueCommand()).Process(command, this.repository));
            }

            if (command.Verb == "get")
            {
                CheckArity(command, 0);
                string key = command.Key;

                return new CommandResult(this.repository.GetValue(key));
            }

            throw new InvalidDataException();
        }

        private static void CheckArity(CommandInfo command, int arity)
        {
            if (arity == 0 && command.Parameters != null && command.Parameters.Count != 0)
                throw new InvalidDataException("0 parameters expected");

            if (arity != 0 && (command.Parameters == null || command.Parameters.Count != arity))
                throw new InvalidDataException(string.Format("{0} parameters expected", arity));
        }
    }
}
