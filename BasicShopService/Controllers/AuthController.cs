using BasicShopService.DTOs.Auth;
using BasicShopService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;

namespace BasicShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public AuthController(IUsuarioService usuarioService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // Autenticar al usuario
            var usuario = await _usuarioService.Autenticar(request.Email, request.Password);
            if (usuario == null || usuario.Estado != EstadoUsuario.Activo)
            {
                return Unauthorized("Credenciales inválidas o usuario inactivo.");
            }

            // Generar el token de autenticación
            var token = await _authService.GenerarToken(usuario);

            // Mapear `Usuario` a `UserDTO`
            var userDto = new UserDTO
            {
                Id = usuario.Id,
                CodigoTrabajador = usuario.CodigoTrabajador,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Puesto = usuario.Puesto,
                Rol = usuario.Rol.ToString()
            };

            // Retornar el token y el usuario en el DTO de respuesta
            var response = new AuthResponseDTO
            {
                Token = token,
                User = userDto
            };

            return Ok(response);
        }
    }
}
