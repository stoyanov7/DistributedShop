namespace DistributedShop.Products.Domain
{
    using DistributedShop.Common.Mongo.Attributes;
    using DistributedShop.Common.Types;
    using System;

    [BsonCollection("products")]
    public class Product : BaseEntity
    {
        public Product(Guid id, string name, string description, string vendor, decimal price, int quantity)
            : base(id)
        {
            this.SetName(name);
            this.SetVendor(vendor);
            this.SetDescription(description);
            this.SetPrice(price);
            this.SetQuantity(quantity);
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Vendor { get; private set; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Product name cannot be empty.");

            this.Name = name.Trim().ToLowerInvariant();
            this.SetUpdatedDate();
        }

        public void SetVendor(string vendor)
        {
            if (string.IsNullOrEmpty(vendor))
                throw new Exception("Product vendor cannot be empty.");

            this.Vendor = vendor.Trim().ToLowerInvariant();
            this.SetUpdatedDate();
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new Exception("Product description cannot be empty.");

            this.Description = description.Trim();
            this.SetUpdatedDate();
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new Exception("Product price cannot be zero or negative.");

            this.Price = price;
            this.SetUpdatedDate();
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new Exception("Product quantity cannot be negative.");

            this.Quantity = quantity;
            this.SetUpdatedDate();
        }
    }
}
