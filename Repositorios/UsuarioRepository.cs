using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public void CrearUsuario(Usuario nuevoUsuario)
        {
            var query = $"INSERT INTO Usuario (id,nombre_de_usuario,contrasenia,rol) VALUES(@Id,@nombre_de_usuario,@contrasenia,@rol)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@Id", nuevoUsuario.IdUsuario));
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nuevoUsuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", nuevoUsuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", nuevoUsuario.Nivel));
                command.ExecuteNonQuery();
                connection.Close();
            }

            if (nuevoUsuario == null)
            {
                throw new Exception("El Usuario no se creo correctamente");
            }

        }
        public List<Usuario> TraerTodosLosUsuarios()
        {
            List<Usuario> listaDeUsuarios = new List<Usuario>();
            var query = "SELECT * FROM usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nuevoUsuario = new Usuario();
                        nuevoUsuario.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        nuevoUsuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        listaDeUsuarios.Add(nuevoUsuario);
                    }
                }
                connection.Close();
            }
            if (listaDeUsuarios == null)
            {
                throw new Exception("Lista de Usuarios no encontrada.");
            }
            return listaDeUsuarios;
        }



        public Usuario TraerUsuarioPorId(int? idRecibe)
        {
            var query = "SELECT * FROM Usuario WHERE id_usuario = @id_usuario";
            var usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    }
                    connection.Close();
                }
                if (usuario == null)
                {
                    throw new Exception("El Usuario no está creado");
                }
            }
            return usuario;
        }

        public void EliminarUsuarioPorId(int? idRecibe)
        {
            TableroRepository repoTab = new TableroRepository();
            repoTab.Inhabilitar(idRecibe);
            var query = "DELETE FROM Usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));
                int usuariobusc = command.ExecuteNonQuery();
                if (usuariobusc == 0)
                {
                    throw new Exception("No se encontró ningun usuario con el ID indicado.");
                }
                connection.Close();

            }
        }

        public void ModificarUsuario(Usuario nuevoUsuario)
        {
            var query = "UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario,contrasenia=@contrasenia,nivel_de_acceso=@nivel WHERE id_usuario = @id_usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario", nuevoUsuario.IdUsuario));
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nuevoUsuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", nuevoUsuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@nivel", nuevoUsuario.Nivel));
                
                int usuariobusc = command.ExecuteNonQuery();
                connection.Close();
                if (usuariobusc == 0)
                {
                    throw new Exception("No se encontró ningun usuario con el ID indicado.");
                }
            }
        }

    }
}