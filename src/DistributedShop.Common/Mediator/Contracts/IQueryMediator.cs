namespace DistributedShop.Common.Mediator.Contracts
{
    using DistributedShop.Common.Mediator.Types;
    using System.Threading.Tasks;

    public interface IQueryMediator
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
