using System;
using Common.Messages;

namespace Common.Messages
{
    public interface ICommandDispatcher
    {
        ICommandResponse Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

