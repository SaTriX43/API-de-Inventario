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
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));

            // 2. Claims del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
            };

            // 3. Crear credenciales de firma
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Descriptor del token (la plantilla)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"],
                SigningCredentials = creds
            };

            // 5. Crear el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Convertir a string
            return tokenHandler.WriteToken(token);
        }
    }

}
