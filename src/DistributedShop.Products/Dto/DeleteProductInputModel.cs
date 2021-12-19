namespace DistributedShop.Products.Dto
{
    using DistributedShop.Common.Mediator.Contracts;
    using System;

    public class DeleteProductInputModel : ICommand
    {
        public Guid Id { get; set; }
    }
}
