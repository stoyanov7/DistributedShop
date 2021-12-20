namespace DistributedShop.Discounts.Handlers
{
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Discounts.Domain;
    using DistributedShop.Discounts.Dto;
    using DistributedShop.Discounts.Repositories;
    using System.Threading.Tasks;

    public class CreateDiscountHandler : ICommandHandler<CreateDiscountInputModel>
    {
        private readonly IDiscountRepository discountsRepository;

        public CreateDiscountHandler(IDiscountRepository discountsRepository)
            => this.discountsRepository = discountsRepository;

        public async Task HandleAsync(CreateDiscountInputModel model)
        {
            var discount = new Discount(model.Id, model.CustomerId, model.Code, model.Percentage);
            await this.discountsRepository.AddAsync(discount);
        }
    }
}
