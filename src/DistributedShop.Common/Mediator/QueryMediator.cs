namespace DistributedShop.Common.Mediator
{
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mediator.Types;
    using System;
    using System.Threading.Tasks;

    public class QueryMediator : IQueryMediator
    {
        private readonly IServiceProvider serviceProvider;

        public QueryMediator(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = serviceProvider.GetService(handlerType);

            return await handler.HandleAsync((dynamic)query);
        }
    }
}
