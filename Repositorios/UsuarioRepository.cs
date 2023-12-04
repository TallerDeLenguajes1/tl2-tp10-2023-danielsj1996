using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public List<Usuario> TraerTodosLosUsuarios()
        {
            var query = "SELECT * FROM usuario;";
            List<Usuario> listaDeUsuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        user.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        user.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        listaDeUsuarios.Add(user);
                    }
                }
                connection.Close();
            }
            return listaDeUsuarios;
        }
        public void CrearUsuario(Usuario nuevoUsuario)
        {
            var query = "INSERT INTO Usuario (nombre_de_usuario) VALUES(@nombre_de_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nuevoUsuario.NombreDeUsuario));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }



        public Usuario TraerUsuarioPorId(int idRecibe)
        {
            var query = "SELECT * FROM Usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));

                var usuario = new Usuario();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    }
                    connection.Close();
                    return usuario;
                }
            }
        }

        public void EliminarUsuarioPorId(int idRecibe)
        {
            var query = "DELETE FROM Usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ModificarUsuario(int idRecibe, Usuario nuevoUsuario)
        {
            var query = "UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario WHERE id_usuario = @id_usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nuevoUsuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}