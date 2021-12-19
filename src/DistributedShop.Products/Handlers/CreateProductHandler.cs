namespace DistributedShop.Products.Handlers
{
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Products.Domain;
    using DistributedShop.Products.Dto;
    using DistributedShop.Products.Repositories;
    using System;
    using System.Threading.Tasks;

    public sealed class CreateProductHandler : ICommandHandler<CreateProductInputModel>
    {
        private readonly IProductRepository productRepository;

        public CreateProductHandler(IProductRepository productRepository) => this.productRepository = productRepository;

        public async Task HandleAsync(CreateProductInputModel model)
        {
            if (model.Quantity < 0)
                throw new Exception("Product quantity cannot be negative.");

            if (await this.productRepository.ExistsAsync(model.Name))
                throw new Exception($"Product: '{model.Name}' already exists.");

            var product = new Product(model.Id, model.Name, model.Description, model.Vendor, model.Price, model.Quantity);
            await productRepository.AddAsync(product);
        }
    }
}
