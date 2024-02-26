using System;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface IUsuarioRepository
    {
        void CrearUsuario(Usuario usuario);
        void ModificarUsuario(int idRecibe, Usuario usuario);
        List<Usuario> TraerTodosLosUsuarios();
        Usuario TraerUsuarioPorId(int id);
        void EliminarUsuarioPorId(int id);
        Usuario ObtenerIDDelUsuarioLogueado(string nombreUsuario, string contrasenia);
        List<Usuario> BuscarUsuarioPorNombre(string nombre);
        bool ExisteUsuario(string nombreDeUsuario);
    }
}