using Microsoft.EntityFrameworkCore;
using ShopBridge.Core;
using ShopBridge.Infra.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Repository
{
    public class ProductRepository: GenericRepository<Product, int>, IProductRepository
    {
        private readonly AppDbContext appDbContext;
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            var products = await appDbContext.Products.Include(y => y.Unit).Where(x => x.Discontinued == false).ToListAsync();
            return products;
        }

        public override async Task<Product> GetById(int id)
        {
            var products = await appDbContext.Products.Include(y => y.Unit).FirstOrDefaultAsync(x => x.Id == id);
            return products;
        }

        public async Task<IEnumerable<Product>> FindBy(string productName)
        {
            var entitiylist = await base.FindBy(x => x.Name == productName);
            return entitiylist;
        }

        public async Task<IEnumerable<Product>> FindByUnitStock(string productName)
        {

            var entitiylist = await base.FindBy(x => x.Name == productName);
            return entitiylist;
        }

        public override PaginatedList<Product> GetAll(int pageIndex, int pageSize)
        {
            IQueryable<Product> query = appDbContext.Products.Include(y => y.Unit).Paginate(pageIndex, pageSize);
            return query.ToPaginatedList(pageIndex, pageSize, query.Count());
        }

    }
}
