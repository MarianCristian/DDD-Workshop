using System;
using Common.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Messaging
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return serviceProvider.GetService<IHandleQuery<TQuery, TResult>>().Handle(query);
        }
    }
}

