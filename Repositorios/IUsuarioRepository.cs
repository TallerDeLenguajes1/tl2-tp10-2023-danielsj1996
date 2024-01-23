using System;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface IUsuarioRepository
    {
        public void CrearUsuario(Usuario usuario);
        public void ModificarUsuario(Usuario usuarioAModificar);
        public Usuario TraerUsuarioPorId(int? id);
        public List<Usuario> TraerTodosLosUsuarios();
        public void EliminarUsuarioPorId(int id);
        
    }
}