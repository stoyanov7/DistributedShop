namespace DistributedShop.Discounts.Domain
{
    using DistributedShop.Common.Mongo.Attributes;
    using DistributedShop.Common.Types;
    using System;

    [BsonCollection("discounts")]
    public class Discount : IIdentifiable
    {
        public Discount(Guid id, Guid customerId, string code, short percentage)
        {
            this.SetId(id);
            this.SetCustomerId(customerId);
            this.SetCode(code);
            this.SetPercentage(percentage);
        }

        public Guid Id { get; private set; }

        public Guid CustomerId { get; private set; }

        public string Code { get; private set; }

        public short Percentage { get; private set; }

        public DateTime? UsedAt { get; private set; }

        public void UseDiscount()
        {
            if (this.UsedAt.HasValue)
                throw new Exception($"Discount: {this.Id} was already applied at: {this.UsedAt}");

            this.UsedAt = DateTime.UtcNow;
        }

        private void SetId(Guid id)
        {
            if (id == null || id == default)
                throw new Exception("Invalid discount id");

            this.Id = id;
        }

        private void SetCustomerId(Guid customerId)
        {
            if (customerId == null || customerId == default)
                throw new Exception("Invalid customer id");

            this.CustomerId = customerId;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new Exception("Empty discount code.");

            this.Code = code;
        }

        private void SetPercentage(short percentage)
        {
            if (percentage < 1 || percentage > 100)
                throw new Exception($"Invalid discount percentage: {percentage}");

            this.Percentage = percentage;
        }
    }
}
