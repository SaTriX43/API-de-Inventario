using API_de_Inventario.DALs;
using API_de_Inventario.DALs.UsuarioRepositoryCarpeta;
using API_de_Inventario.DTOs.AutenticacionDtoCarpeta;
using API_de_Inventario.Models;
using API_de_Inventario.Models.Enums;
using InventarioAPI.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_de_Inventario.Services.AutenticacionServiceCarpeta
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IConfiguration _config;

        public AutenticacionService(
            IUsuarioRepository usuarioRepository,
            IUnidadDeTrabajo unidadDeTrabajo,
            IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
            _config = config;
        }

        public async Task<Result<AutenticacionRespuestaDto>> RegisterAsync(RegisterDto dto)
        {
            var existe = await _usuarioRepository.ObtenerPorEmailAsync(dto.Email);
            if (existe != null)
                return Result<AutenticacionRespuestaDto>.Failure("El email ya existe");

            var usuario = new Usuario
            {
                Email = dto.Email,
                Name = dto.Nombre,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = RolUsuario.Operador
            };

            _usuarioRepository.Crear(usuario);
            await _unidadDeTrabajo.GuardarCambiosAsync();

            var token = GenerarJwt(usuario);

            return Result<AutenticacionRespuestaDto>.Success(new AutenticacionRespuestaDto
            {
                AccessToken = token
            });
        }

        public async Task<Result<AutenticacionRespuestaDto>> LoginAsync(LoginDto dto)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(dto.Email);
            if (usuario == null)
                return Result<AutenticacionRespuestaDto>.Failure("Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return Result<AutenticacionRespuestaDto>.Failure("Credenciales inválidas");

            var token = GenerarJwt(usuario);

            return Result<AutenticacionRespuestaDto>.Success(new AutenticacionRespuestaDto
            {
                AccessToken = token
            });
        }

        private string GenerarJwt(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["Jwt:ExpiresInMinutes"]!)
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
