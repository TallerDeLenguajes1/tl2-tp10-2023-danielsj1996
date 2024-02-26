using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private readonly string CadenaConexion;

        public TableroRepository(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;

        }



        public void CrearTablero(Tablero nuevoTablero)
        {

            var query = "INSERT INTO Tablero (id_usuario_propietario, nombre_tablero, descripcion_tablero)" +
                        " VALUES (@idPropietario, @nombreTablero, @descripTablero);";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idPropietario", nuevoTablero.IdUsuarioPropietario));
                    command.Parameters.Add(new SQLiteParameter("@nombreTablero", nuevoTablero.NombreDeTablero));
                    command.Parameters.Add(new SQLiteParameter("@descripTablero", nuevoTablero.DescripcionDeTablero));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema en la BD al crear un nuevo Tablero.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Tablero> ListarTodosTableros()
        {
            var query = "SELECT * FROM Tablero INNER JOIN Usuario ON Tablero.id_usuario_propietario = Usuario.id_usuario";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tablero = new Tablero
                            {
                                IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                NombreDeTablero = reader["nombre_tablero"].ToString()!,
                                DescripcionDeTablero = reader["descripcion_tablero"].ToString(),
                                IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                                NombreDePropietario = reader["nombre_de_usuario"].ToString()!
                            };
                            tableros.Add(tablero);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD la lista de Tableros");
                }
                finally
                {
                    connection.Close();
                }
            }
            if (tableros == null)
            {
                throw new Exception("Lista de Tableros vacia");
            }
            return tableros;
        }
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int idRecibe)
        {
            var query = "SELECT * FROM Tablero INNER JOIN Usuario ON Tablero.id_usuario_propietario = Usuario.id_usuario " +
             "WHERE id_usuario_propietario = @idUsuario;";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idUsuario", idRecibe));
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tablero = new Tablero
                            {
                                IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                NombreDeTablero = reader["nombre_tablero"].ToString()!,
                                DescripcionDeTablero = reader["descripcion_tablero"].ToString(),
                                IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                                NombreDePropietario = reader["nombre_de_usuario"].ToString()!
                            };
                            tableros.Add(tablero);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD la lista de Tableros");
                }
                finally
                {
                    connection.Close();
                }
            }
            if (tableros == null)
            {
                throw new Exception("Lista de Tablero vacia");
            }
            return tableros;
        }
        public void CambiarPropietarioTableros(Tablero tablero)
        {
            var query = "UPDATE Tablero SET nombre_tablero = @nombre, descripcion_tablero = @descripcion, " +
            "id_usuario_propietario = @idUsuarioPropietario WHERE id_tablero = @idTablero";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", tablero.NombreDeTablero);
                    command.Parameters.AddWithValue("@descripcion", tablero.DescripcionDeTablero);
                    command.Parameters.AddWithValue("@idUsuarioPropietario", tablero.IdUsuarioPropietario);
                    command.Parameters.AddWithValue("@idTablero", tablero.IdTablero);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al cambiar el propietario de los tableros: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Tablero> BuscarTablerosPorPropietario(int idPropietario)
        {
            var query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idPropietario";
            List<Tablero> tablerosAsociados = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idPropietario", idPropietario));

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tablero = new Tablero
                            {
                                IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                NombreDeTablero = reader["nombre_tablero"].ToString()!,
                                DescripcionDeTablero = reader["descripcion_tablero"].ToString(),
                                IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"])
                            };
                            tablerosAsociados.Add(tablero);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al buscar tableros por propietario en la base de datos");
                }
                finally
                {
                    connection.Close();
                }
            }
            return tablerosAsociados;
        }

        public Tablero ObtenerTableroPorId(int idRecibe)
        {
            var query = "SELECT * FROM Tablero INNER JOIN Usuario ON Tablero.id_usuario_propietario = Usuario.id_usuario WHERE id_tablero = @idTablero;";
            Tablero tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idTablero", idRecibe));
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                            tablero.NombreDeTablero = reader["nombre_tablero"].ToString()!;
                            tablero.DescripcionDeTablero = reader["descripcion_tablero"].ToString();
                            tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                            tablero.NombreDePropietario = reader["nombre_de_usuario"].ToString()!;
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al extraer desde la BD el tablero");
                }
                finally
                {
                    connection.Close();
                }
            }
            if (tablero == null)
            {
                throw new Exception("Tablero no existe");
            }
            return tablero;
        }
        public void EliminarTableroYTareas(int idTablero)
        {
            string queryEliminarTareas = "DELETE FROM Tarea WHERE id_tablero = @idTablero;";
            string queryEliminarTablero = "DELETE FROM Tablero WHERE id_tablero = @idTablero;";

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(queryEliminarTareas, connection))
                {
                    command.Parameters.AddWithValue("@idTablero", idTablero);
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(queryEliminarTablero, connection))
                {
                    command.Parameters.AddWithValue("@idTablero", idTablero);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public void ModificarTablero(int idRecibe, Tablero modificarTablero)
        {
            var query = "UPDATE Tablero SET id_usuario_propietario = @idPropietario, nombre_tablero = @nombreTablero, " +
            "descripcion_tablero = @descripTablero WHERE id_tablero = @idRecibe;";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@idPropietario", modificarTablero.IdUsuarioPropietario));
                    command.Parameters.Add(new SQLiteParameter("@nombreTablero", modificarTablero.NombreDeTablero));
                    command.Parameters.Add(new SQLiteParameter("@descripTablero", modificarTablero.DescripcionDeTablero));
                    command.Parameters.Add(new SQLiteParameter("@idRecibe", idRecibe));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al Modificar el Tablero");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public List<Tablero> BuscarTablerosPorNombre(string nombre)
        {
            var query = "SELECT * FROM Tablero INNER JOIN Usuario ON Tablero.id_usuario_propietario = Usuario.id_usuario WHERE nombre_tablero LIKE @nombre";
            List<Tablero> listaDeTableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                try
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.Add(new SQLiteParameter("@nombre", "%" + nombre + "%"));

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tablero = new Tablero
                            {
                                IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                NombreDeTablero = reader["nombre_tablero"].ToString()!,
                                DescripcionDeTablero = reader["descripcion_tablero"].ToString(),
                                IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]),
                                NombreDePropietario = reader["nombre_de_usuario"].ToString()!
                            };
                            listaDeTableros.Add(tablero);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un problema al buscar tableros por nombre en la base de datos");
                }
                finally
                {
                    connection.Close();
                }
            }
            return listaDeTableros;
        }
    }

}


