namespace DistributedShop.Common.Mediator
{
    using DistributedShop.Common.Mediator.Contracts;
    using DistributedShop.Common.Mediator.Types;
    using System.Threading.Tasks;

    public class Mediator : IMediator
    {
        private readonly IQueryMediator queryMediator;
        private readonly ICommandMediator commandMediator;

        public Mediator(IQueryMediator queryMediator, ICommandMediator commandMediator)
        {
            this.queryMediator = queryMediator;
            this.commandMediator = commandMediator;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) => await this.queryMediator.QueryAsync(query);

        public async Task SendAsync<TModel>(TModel model) where TModel : ICommand
            => await this.commandMediator.SendAsync(model);
    }
}
