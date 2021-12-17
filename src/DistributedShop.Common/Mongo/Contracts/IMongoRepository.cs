namespace DistributedShop.Common.Mongo.Contracts
{
    using DistributedShop.Common.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMongoRepository<TEntity> where TEntity : IIdentifiable
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(Guid id);
    }
}
