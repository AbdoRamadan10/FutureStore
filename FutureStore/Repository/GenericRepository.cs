using FutureStore.Data;
using FutureStore.Helpers;
using FutureStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutureStore.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FutureStoreContext _dBContext;
        public GenericRepository(FutureStoreContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void Add(TEntity entity)
        {
            _dBContext.Set<TEntity>().Add(entity);
            _dBContext.SaveChanges();
        }
        public void AddMany(IEnumerable<TEntity> entities)
        {
            _dBContext.Set<TEntity>().AddRange(entities);
            _dBContext.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            _dBContext.Set<TEntity>().Remove(entity);
            _dBContext.SaveChanges();
        }
        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Find(predicate);
            _dBContext.Set<TEntity>().RemoveRange(entities);
            _dBContext.SaveChanges();
        }
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }
    

        public TEntity FindWithInclude(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>? include =null, FindOptions? findOptions = null)
        {
            if (include != null)
            {
                return Get(findOptions).Include(include).FirstOrDefault(predicate)!;
            }
            return Get(findOptions).FirstOrDefault(predicate)!;
        }

        public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }
        public void Update(TEntity entity)
        {
            _dBContext.Set<TEntity>().Update(entity);
            _dBContext.SaveChanges();
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dBContext.Set<TEntity>().Any(predicate);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dBContext.Set<TEntity>().Count(predicate);
        }
        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _dBContext.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            
           
            return entity;
        }
    }
}
