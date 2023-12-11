using System;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface IUsuarioRepository
    {
        public void CrearUsuario(Usuario usuario);
        public void ModificarUsuario(int idRecibe, Usuario usuario);
        public List<Usuario> TraerTodosLosUsuarios();
        public Usuario TraerUsuarioPorId(int id);
        public void EliminarUsuarioPorId(int id);
        
    }
}