using API_de_Inventario.Models;

namespace API_de_Inventario.DALs.UsuarioRepositoryCarpeta
{
    public interface IUsuarioRepository
    {
        public Task<Usuario?> ObtenerPorEmailAsync(string email);
        public void Crear(Usuario usuario);
    }

}
