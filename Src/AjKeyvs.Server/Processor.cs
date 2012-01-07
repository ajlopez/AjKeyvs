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

        private static IDictionary<string, ICommand> commands;

        private CommandReader reader;
        private Repository repository;

        static Processor()
        {
            commands = new Dictionary<string, ICommand>();

            commands["set"] = new SetValueCommand();
            commands["get"] = new GetValueCommand();

            commands["sadd"] = new SetAddMemberCommand();
            commands["srem"] = new SetRemoveMemberCommand();
            commands["sismember"] = new SetIsMemberCommand();
        }

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
            CommandInfo info = this.reader.NextCommand();

            if (info == null)
                return null;

            if (!commands.ContainsKey(info.Verb))
                throw new InvalidDataException();

            return commands[info.Verb].Process(info, this.repository);
        }
    }
}
