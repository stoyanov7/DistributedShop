namespace DistributedShop.Products.Handlers
{
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Products.Dto;
    using DistributedShop.Products.Repositories;
    using Mapster;
    using System.Threading.Tasks;

    public class GetProductHandler : IQueryHandler<GetProductInputModel, GetProductViewModel>
    {
        private readonly IProductRepository productRepository;

        public GetProductHandler(IProductRepository productRepository) => this.productRepository = productRepository;

        public async Task<GetProductViewModel> HandleAsync(GetProductInputModel query)
        {
            var product = await this.productRepository.GetByIdAsync(query.Id);

            return product?.Adapt<GetProductViewModel>();
        }
    }
}
