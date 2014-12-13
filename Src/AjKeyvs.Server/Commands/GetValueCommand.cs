namespace AjKeyvs.Server.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GetValueCommand : AjKeyvs.Server.Commands.ICommand
    {
        public CommandResult Process(CommandInfo info, Repository repository)
        {
            info.CheckArity(0);
            string key = info.Key;

            return new CommandResult(repository.GetValue(key));
        }
    }
}
