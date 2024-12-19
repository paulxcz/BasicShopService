using BasicShopService.Models;

namespace BasicShopService.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto> Add(Producto producto);
        Task<Producto> GetById(int id);
        Task<IEnumerable<Producto>> GetAll();
        Task<IEnumerable<Producto>> FindByCriteria(string criterio);
        Task<Producto> Update(Producto producto);
        Task<bool> Delete(int id);
    }
}
