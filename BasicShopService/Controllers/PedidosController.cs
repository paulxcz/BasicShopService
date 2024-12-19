using BasicShopService.DTOs.Pedidos;
using BasicShopService.DTOs.Usuarios;
using BasicShopService.Models;
using BasicShopService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using System.Security.Claims;

namespace BasicShopService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IUsuarioService _usuarioService;

        public PedidosController(IPedidoService pedidoService, IUsuarioService usuarioService)
        {
            _pedidoService = pedidoService;
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> ObtenerPedido(int id)
        {
            var pedido = await _pedidoService.ObtenerPedidoPorId(id);
            if (pedido == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Delivery" && pedido.DeliveryId.ToString() != userId)
            {
                return Forbid();
            }

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                FechaPedido = pedido.FechaPedido,
                FechaDespacho = pedido.FechaDespacho,
                FechaEntrega = pedido.FechaEntrega,
                VendedorId = pedido.VendedorId,
                DeliveryId = pedido.DeliveryId,
                Estado = pedido.Estado
            };

            return Ok(pedidoDTO);
        }

        [Authorize(Roles = "Vendedor")]
        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> CrearPedido([FromBody] CrearPedidoDTO crearPedidoDTO)
        {
  
            var pedido = new Pedido
            {
                NumeroPedido = crearPedidoDTO.NumeroPedido,
                FechaPedido = crearPedidoDTO.FechaPedido,
                VendedorId = crearPedidoDTO.VendedorId,
                DeliveryId = crearPedidoDTO.DeliveryId,
                Estado = crearPedidoDTO.Estado
            };

            var productos = crearPedidoDTO.Productos.Select(p => new PedidoProducto
            {
                ProductoId = p.ProductoId,
                Cantidad = p.Cantidad
            }).ToList();

            var nuevoPedido = await _pedidoService.CrearPedido(pedido, productos);

            var pedidoDTO = new PedidoDTO
            {
                Id = nuevoPedido.Id,
                NumeroPedido = nuevoPedido.NumeroPedido,
                FechaPedido = nuevoPedido.FechaPedido,
                FechaDespacho = nuevoPedido.FechaDespacho,
                FechaEntrega = nuevoPedido.FechaEntrega,
                VendedorId = nuevoPedido.VendedorId,
                DeliveryId = nuevoPedido.DeliveryId,
                Estado = nuevoPedido.Estado
            };

            return CreatedAtAction(nameof(ObtenerPedido), new { id = pedidoDTO.Id }, pedidoDTO);
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoPedido(int id, [FromBody] ActualizarEstadoPedidoDTO estadoDTO)
        {
            var pedido = await _pedidoService.ActualizarEstadoPedido(id, estadoDTO.Estado);
            if (pedido == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var result = await _pedidoService.EliminarPedido(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/detalle")]
        public async Task<ActionResult<PedidoConDetalleDTO>> ObtenerPedidoConDetalle(int id)
        {
            var pedidoConDetalle = await _pedidoService.ObtenerPedidoConDetalle(id);
            if (pedidoConDetalle == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Delivery" && pedidoConDetalle.DeliveryId.ToString() != userId)
            {
                return Forbid();
            }

            return Ok(pedidoConDetalle);
        }

        [HttpPost("listar")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> ListarPedidos([FromBody] ListarPedidosRequesDTO request)
        {
            var pedidos = await _pedidoService.ListarTodosLosPedidos(request.NumeroPedido);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Si el rol es "Delivery", filtrar los pedidos asociados a su ID
            if (userRole == "Delivery")
            {
                pedidos = pedidos.Where(p => p.DeliveryId.ToString() == userId).ToList();
            }

            var pedidosDTO = pedidos.Select(p => new PedidoDTO
            {
                Id = p.Id,
                NumeroPedido = p.NumeroPedido,
                FechaPedido = p.FechaPedido,
                FechaDespacho = p.FechaDespacho,
                FechaEntrega = p.FechaEntrega,
                VendedorId = p.VendedorId,
                DeliveryId = p.DeliveryId,
                Estado = p.Estado
            }).ToList();

            return Ok(pedidosDTO);
        }

        [Authorize(Roles = "Vendedor")]
        [HttpGet("usuarios-delivery-activos")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuariosDeliveryActivos()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosDeliveryActivos();
            var usuariosDTO = usuarios.Select(u => new DeliveryActivoDTO
            {
                Id = u.Id,
                CodigoTrabajador = u.CodigoTrabajador,
                Nombre = u.Nombre
            });

            return Ok(usuariosDTO);
        }
    }
}
