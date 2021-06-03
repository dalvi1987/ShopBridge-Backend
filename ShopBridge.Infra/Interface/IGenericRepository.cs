using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Infra.Interface
{
    public interface IGenericRepository<T, TKey> where T:class
    {
        public Task<T> Create(T _object);
        public void Update(T _object);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(TKey Id);
        public Task Delete(TKey Id);
    }
}
