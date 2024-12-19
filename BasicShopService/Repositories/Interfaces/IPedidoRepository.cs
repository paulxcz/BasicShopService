using Proyecto.Models;

namespace BasicShopService.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> Add(Pedido pedido);
        Task<Pedido> GetById(int id);
        Task<IEnumerable<Pedido>> GetAll();
        Task<IEnumerable<Pedido>> FindByNumeroPedido(string numeroPedido);
        Task<Pedido> Update(Pedido pedido);
        Task<bool> Delete(int id);
        Task<Pedido> UpdateEstado(int id, EstadoPedido nuevoEstado);

    }
}
