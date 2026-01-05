using API_de_Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.DALs.UsuarioRepositoryCarpeta
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public void Crear(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }
    }

}
