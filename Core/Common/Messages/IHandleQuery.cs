using System;
namespace Common.Messages
{
    public interface IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}

