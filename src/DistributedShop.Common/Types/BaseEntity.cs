namespace DistributedShop.Common.Types
{
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public abstract class BaseEntity : IIdentifiable
    {
        public BaseEntity(Guid id)
        {
            this.Id = id;
            this.CreatedDate = DateTime.UtcNow;
            this.SetUpdatedDate();
        }

        [BsonId]
        public Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime UpdatedDate { get; protected set; }

        protected virtual void SetUpdatedDate() => this.UpdatedDate = DateTime.UtcNow;
    }
}
