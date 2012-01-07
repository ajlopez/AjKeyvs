namespace AjKeyvs.Server.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class SetRemoveMemberCommand : AjKeyvs.Server.Commands.ICommand
    {
        private static CommandResult ok = new CommandResult();

        public CommandResult Process(CommandInfo info, Repository repository)
        {
            info.CheckArity(1);
            string key = info.Key;

            if (info.Parameters[0] is ulong)
            {
                ulong value = (ulong)info.Parameters[0];
                repository.SetRemoveMember(key, value);
            }
            else
            {
                string value = (string)info.Parameters[0];
                repository.SetRemoveMember(key, value);
            }

            return ok;
        }
    }
}
