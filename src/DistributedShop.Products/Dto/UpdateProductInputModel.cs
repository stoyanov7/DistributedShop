namespace DistributedShop.Products.Dto
{
    using DistributedShop.Common.Mediator.Contracts;
    using System;

    public class UpdateProductInputModel : ICommand
    {
        public UpdateProductInputModel(Guid id, string name, string description, string vendor, decimal price, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Vendor = vendor;
            this.Price = price;
            this.Quantity = quantity;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Vendor { get; }

        public decimal Price { get; }

        public int Quantity { get; }
    }
}
