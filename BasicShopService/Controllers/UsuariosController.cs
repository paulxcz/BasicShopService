using BasicShopService.DTOs;
using BasicShopService.DTOs.Usuarios;
using BasicShopService.Helpers;
using BasicShopService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using Proyecto.Models;
using System.Data;

namespace BasicShopService.Controllers
{
    [Authorize(Roles = "Encargado")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuarios();

            var usuariosDTO = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                CodigoTrabajador = u.CodigoTrabajador,
                Nombre = u.Nombre,
                Email = u.Email,
                Telefono = u.Telefono,
                Puesto = u.Puesto,
                Rol = u.Rol,
                Estado = u.Estado
            });

            return Ok(usuariosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound();

            var usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                CodigoTrabajador = usuario.CodigoTrabajador,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Puesto = usuario.Puesto,
                Rol = usuario.Rol,
                Estado = usuario.Estado
            };

            return Ok(usuarioDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CrearUsuario([FromBody] CrearUsuarioDTO crearUsuarioDTO)
        {
            var usuario = new Usuario
            {
                CodigoTrabajador = crearUsuarioDTO.CodigoTrabajador,
                Nombre = crearUsuarioDTO.Nombre,
                Email = crearUsuarioDTO.Email,
                Telefono = crearUsuarioDTO.Telefono,
                Puesto = crearUsuarioDTO.Puesto,
                Rol = crearUsuarioDTO.Rol,
                Estado = crearUsuarioDTO.Estado,
                PasswordHash = PasswordHasher.HashPassword(crearUsuarioDTO.Password)
            };

            var nuevoUsuario = await _usuarioService.CrearUsuario(usuario);
            var usuarioDTO = new UsuarioDTO
            {
                Id = nuevoUsuario.Id,
                CodigoTrabajador = nuevoUsuario.CodigoTrabajador,
                Nombre = nuevoUsuario.Nombre,
                Email = nuevoUsuario.Email,
                Telefono = nuevoUsuario.Telefono,
                Puesto = nuevoUsuario.Puesto,
                Rol = nuevoUsuario.Rol,
                Estado = nuevoUsuario.Estado
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDTO.Id }, usuarioDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] ActualizarUsuarioDTO actualizarUsuarioDTO)
        {
            var usuarioExistente = await _usuarioService.ObtenerUsuarioPorId(id);
            if (usuarioExistente == null)
                return NotFound();

            // Mapear ActualizarUsuarioDTO a Usuario
            usuarioExistente.CodigoTrabajador = actualizarUsuarioDTO.CodigoTrabajador;
            usuarioExistente.Nombre = actualizarUsuarioDTO.Nombre;
            usuarioExistente.Email = actualizarUsuarioDTO.Email;
            usuarioExistente.Telefono = actualizarUsuarioDTO.Telefono;
            usuarioExistente.Puesto = actualizarUsuarioDTO.Puesto;
            usuarioExistente.Rol = actualizarUsuarioDTO.Rol;
            usuarioExistente.Estado = actualizarUsuarioDTO.Estado;

            await _usuarioService.ActualizarUsuario(usuarioExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var resultado = await _usuarioService.EliminarUsuario(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("deliveryActivos")]
        public async Task<ActionResult<IEnumerable<DeliveryActivoDTO>>> GetUsuariosDeliveryActivos()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosDeliveryActivos();
            var deliveryActivoDTO = usuarios.Select(u => new DeliveryActivoDTO
            {
                Id = u.Id,
                CodigoTrabajador = u.CodigoTrabajador,
                Nombre = u.Nombre
            });

            return Ok(deliveryActivoDTO);
        }
    }
}
