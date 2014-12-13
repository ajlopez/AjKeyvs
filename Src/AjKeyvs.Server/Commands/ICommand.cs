namespace AjKeyvs.Server.Commands
{
    using System;

    public interface ICommand
    {
        CommandResult Process(CommandInfo info, Repository repository);
    }
}
