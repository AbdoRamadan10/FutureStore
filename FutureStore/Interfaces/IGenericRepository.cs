using FutureStore.Helpers;
using System.Linq.Expressions;

namespace FutureStore.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(FindOptions? findOptions = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        public TEntity FindWithInclude(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include, FindOptions? findOptions = null);
        void Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
