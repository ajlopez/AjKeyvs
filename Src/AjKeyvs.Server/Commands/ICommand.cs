namespace AjKeyvs.Server.Commands
{
    using System;

    interface ICommand
    {
        CommandResult Process(CommandInfo info, Repository repository);
    }
}
