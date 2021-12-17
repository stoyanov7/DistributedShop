namespace DistributedShop.Common.Mediator.Types
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TModel>
    {
        Task HandleAsync(TModel model);
    }
}
