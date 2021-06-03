using System;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IUnitRepository UnitRepository { get; }
        IProductRepository ProductRepository { get; }

        Task<bool> SaveAsync();
    }
}
