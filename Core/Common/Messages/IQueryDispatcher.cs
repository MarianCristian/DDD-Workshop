using System;
using Common.Messages;

namespace Common.Messages
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}

