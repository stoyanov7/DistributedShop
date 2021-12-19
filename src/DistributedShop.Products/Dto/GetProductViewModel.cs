namespace DistributedShop.Products.Dto
{
    using System;

    public class GetProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Vendor { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
