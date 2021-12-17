namespace DistributedShop.Common.Mediator.Contracts
{
    using System.Threading.Tasks;

    public interface ICommandMediator
    {
        public Task SendAsync<TModel>(TModel model) where TModel : ICommand;
    }
}
