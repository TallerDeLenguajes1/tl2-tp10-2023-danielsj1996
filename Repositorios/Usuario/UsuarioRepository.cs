using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string CadenaConexion;
        public UsuarioRepository(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }
        public void CrearUsuario(Usuario nuevoUsuario)
        {
            var query = "INSERT INTO Usuario (nombre_de_usuario, contrasenia, nivel_de_acceso) VALUES (@nombre, @contrasenia, @nivel)";
            Console.WriteLine("Consulta SQL: " + query);
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@nombre", nuevoUsuario.NombreDeUsuario));
                    command.Parameters.Add(new SQLiteParameter("@contrasenia", nuevoUsuario.Contrasenia));
                    command.Parameters.Add(new SQLiteParameter("@nivel", nuevoUsuario.Nivel));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema en la BD al crear un nuevo usuario.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Usuario> TraerTodosLosUsuarios()
        {
            var query = "SELECT * FROM Usuario;";
            List<Usuario> listaDeUsuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
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
                            nuevoUsuario.Nivel = (NivelDeAcceso)Convert.ToInt32(reader["nivel_de_acceso"]);
                            listaDeUsuarios.Add(nuevoUsuario);
                        }
                    }
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD Los usuarios");
                }
                finally
                {
                    connection.Close();
                }
            }
            if (listaDeUsuarios == null)
            {
                throw new Exception("Lista de Usuarios vacia");
            }
            return listaDeUsuarios;
        }



        public Usuario TraerUsuarioPorId(int idRecibe)
        {
            var usuario = new Usuario();
            var query = "SELECT * FROM Usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
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
                            usuario.NombreDeUsuario = reader["contrasenia"].ToString();
                            usuario.Nivel = (NivelDeAcceso)Convert.ToInt32(reader["nivel_de_acceso"]);
                        }
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD al usuario");
                }
                finally
                {
                    connection.Close();
                }
            }
            if (usuario == null)
            {
                throw new Exception("Usuario no Existe");
            }
            return usuario;
        }

        public void EliminarUsuarioPorId(int idRecibe)
        {
            var query = "DELETE FROM Usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));
                    command.ExecuteNonQuery();

                }

                catch (Exception)
                {
                    throw new Exception("Hubo un problema al borrar al usuario");
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public void ModificarUsuario(int idRecibe, Usuario nuevoUsuario)
        {
            var query = "UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario,contrasenia=@contrasenia,nivel_de_acceso=@nivel WHERE id_usuario = @id_usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nuevoUsuario.NombreDeUsuario));
                    command.Parameters.Add(new SQLiteParameter("@contrasenia", nuevoUsuario.Contrasenia));
                    command.Parameters.Add(new SQLiteParameter("@nivel", (int)nuevoUsuario.Nivel));
                    command.Parameters.Add(new SQLiteParameter("@id_usuario", idRecibe));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al Modificar al usuario");
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public Usuario ObtenerIDDelUsuarioLogueado(string nombre, string contrasenia)
        {

            string query = "SELECT * FROM Usuario WHERE nombre_de_usuario=@nombre AND contrasenia=@contrasenia";
            Usuario usuarioEncontrado = null;

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                        command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuarioEncontrado = new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                    NombreDeUsuario = reader["nombre_de_usuario"].ToString(),
                                    Contrasenia = reader["contrasenia"].ToString(),
                                    Nivel = (NivelDeAcceso)Convert.ToInt32(reader["nivel_de_acceso"]),
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD las credenciales del usuario");
                }
                finally
                {
                    connection.Close();
                }
            }

            return usuarioEncontrado;
        }

        public List<Usuario> BuscarUsuarioPorNombre(string nombre)
        {
            var query = "SELECT * FROM Usuario WHERE nombre_de_usuario LIKE @nombre";
            List<Usuario> listaDeUsuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {

                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("nombre", "%" + nombre + "%"));
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                NombreDeUsuario = reader["nombre_de_usuario"].ToString(),
                                Contrasenia = reader["contrasenia"].ToString(),
                                Nivel = (NivelDeAcceso)Convert.ToInt32(reader["nivel_de_acceso"])

                            };
                            listaDeUsuarios.Add(usuario);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al buscar usuarios por nombre en la base de datos");
                }
                finally
                {
                    connection.Close();
                }
            }
            return listaDeUsuarios;
        }

        public bool ExisteUsuario(string nombreDeUsuario)
        {

            bool existe = false;
            var query = "SELECT * FROM usuario WHERE nombre_de_usuario=@nombre";

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@nombre", nombreDeUsuario));
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            existe = true;
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al buscar el nombre del usuario en la base de datos");
                }
                finally
                {
                    connection.Close();
                }
            }
            return existe;
        }






    }






}






