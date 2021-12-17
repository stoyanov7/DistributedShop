namespace DistributedShop.Common.Mongo
{
    using DistributedShop.Common.Mongo.Attributes;
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Common.Types;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class MongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : IIdentifiable
    {
        protected IMongoCollection<TEntity> Collection { get; }

        public MongoRepository(IMongoDatabase database)
            => this.Collection = database.GetCollection<TEntity>(this.GetCollectionName(typeof(TEntity)));

        public async Task<TEntity> GetByIdAsync(Guid id) => await this.GetAsync(e => e.Id == id);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await this.Collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await this.Collection.Find(predicate).ToListAsync();

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await this.Collection.Find(predicate).AnyAsync();

        public async Task<IQueryable<TEntity>> BrowseAsync(Expression<Func<TEntity, bool>> predicate)
            => await Task.FromResult(this.Collection.AsQueryable().Where(predicate));

        public async Task AddAsync(TEntity entity) => await this.Collection.InsertOneAsync(entity);        

        public async Task UpdateAsync(TEntity entity)
            => await this.Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);        

        public async Task DeleteAsync(Guid id) => await this.Collection.DeleteOneAsync(e => e.Id == id);

        private string GetCollectionName(Type documentType)
            => ((BsonCollectionAttribute)documentType
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())
            ?.CollectionName;
    }
}
