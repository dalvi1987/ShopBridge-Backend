using ShopBridge.Infra.Interface;
using System;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        private bool _disposed;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IUnitRepository UnitRepository => new UnitRepository(appDbContext);

        public IProductRepository ProductRepository => new ProductRepository(appDbContext);

        public async Task<bool> SaveAsync()
        {
            return await appDbContext.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    appDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
