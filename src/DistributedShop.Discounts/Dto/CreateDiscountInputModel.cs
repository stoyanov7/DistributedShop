namespace DistributedShop.Discounts.Dto
{
    using DistributedShop.Common.Mediator.Contracts;
    using System;

    public class CreateDiscountInputModel : ICommand
    {
        public CreateDiscountInputModel(Guid id, Guid customerId, string code, short percentage)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.Code = code;
            this.Percentage = percentage;
        }

        public Guid Id { get; }

        public Guid CustomerId { get; }

        public string Code { get; }

        public short Percentage { get; }
    }
}
