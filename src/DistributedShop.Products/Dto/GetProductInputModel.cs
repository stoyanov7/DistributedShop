namespace DistributedShop.Products.Dto
{
    using DistributedShop.Common.Mediator.Types;
    using System;

    public class GetProductInputModel : IQuery<GetProductViewModel>
    {
        public Guid Id { get; set; }
    }
}
