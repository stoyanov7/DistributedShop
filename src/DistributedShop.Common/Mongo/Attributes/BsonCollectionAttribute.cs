namespace DistributedShop.Common.Mongo.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public BsonCollectionAttribute(string collectionName) => this.CollectionName = collectionName;

        public string CollectionName { get; }        
    }
}
