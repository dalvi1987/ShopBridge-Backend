using ShopBridge.Core;
using ShopBridge.Infra.Interface;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Repository
{
    public class UnitRepository: GenericRepository<Unit, short>, IUnitRepository
    {
        private readonly AppDbContext appDbContext;
        public UnitRepository(AppDbContext appDbContext): base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
