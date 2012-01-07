﻿namespace AjKeyvs.Server.Commands
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
            ulong value = (ulong) info.Parameters[0];

            repository.SetRemoveValue(key, value);
            return ok;
        }
    }
}
