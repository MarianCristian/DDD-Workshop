using System;
namespace Common.Messages
{
    public interface IValidateCommand<TCommand> where TCommand : ICommand
    {
        ICommandResponse Validate(TCommand command);
    }
}