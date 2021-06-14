using Microsoft.EntityFrameworkCore;
using ShopBridge.Core;
using ShopBridge.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Repository
{
    public abstract class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly AppDbContext appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<T> Create(T obj)
        {
            var result = await this.appDbContext.Set<T>().AddAsync(obj);
            return result.Entity;            
        }

        public async Task Delete(TKey id)
        {
            var entity = await this.GetById(id);
            this.appDbContext.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var entitiylist = await this.appDbContext.Set<T>().ToListAsync();
            return entitiylist;
        }

        public virtual async Task<T> GetById(TKey id)
        {
            return await appDbContext.Set<T>().FindAsync(id);
        }

        public void Update(T obj)
        {
            appDbContext.Set<T>().Update(obj);
        }

        public virtual async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            var entitiylist = await appDbContext.Set<T>().Where(predicate).ToListAsync(); 
            return entitiylist;
        }

        public virtual PaginatedList<T> GetAll(int pageIndex, int pageSize)
        {
            IQueryable<T> query = appDbContext.Set<T>().Paginate(pageIndex, pageSize);
            return query.ToPaginatedList(pageIndex, pageSize, query.Count());
        }
    }
}
