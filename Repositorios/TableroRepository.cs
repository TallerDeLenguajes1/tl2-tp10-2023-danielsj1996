using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public void CrearTablero(Tablero nuevoTablero)
        {
            var query = "INSERT INTO Tablero (id,id_usuario_propietario, nombre_tablero, descripcion_tablero) VALUES (@idTablero,@idPropietario, @nombreTablero, @descripTablero);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", nuevoTablero.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@idPropietario", nuevoTablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", nuevoTablero.NombreDeTablero));
                command.Parameters.Add(new SQLiteParameter("@descripTablero", nuevoTablero.DescripcionDeTablero));
                command.ExecuteNonQuery();
                connection.Close();
            }
            if (nuevoTablero == null)
            {
                throw new Exception("El Tablero no se pudo crear,revise los datos e intente nuevamente");
            }
        }

        public void EliminarTableroPorId(int idRecibe)
        {
            var query = "DELETE FROM Tablero WHERE id_tablero = @idRecibe";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idRecibe", idRecibe));
                int filaAfectada = command.ExecuteNonQuery();
                connection.Close();
                if (filaAfectada == 0)
                {
                    throw new Exception("No se encontró ningun tablero con el ID proporcionado");
                }
            }
        }


        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int idUsuario)
        {
            var query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();

                        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tablero.NombreDeTablero = reader["nombre_tablero"].ToString();
                        tablero.DescripcionDeTablero = reader["descripcion_tablero"].ToString();
                        tableros.Add(tablero);
                    }

                }
                if (tableros == null)
                {
                    throw new Exception("El usuario Solicitado no tiene Tableros asignados");
                }
                return tableros;
            }

        }

        public List<Tablero> ListarTodosTableros()
        {
            var query = "SELECT * FROM Tablero";
            List<Tablero> listaDeTablero = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tablero.NombreDeTablero = reader["nombre_tablero"].ToString();
                        tablero.DescripcionDeTablero = reader["descripcion_tablero"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        listaDeTablero.Add(tablero);
                    }
                }
                connection.Close();
            }
            if (listaDeTablero == null)
            {
                throw new Exception("Lista de Tableros no encontrada o Vacìa");
            }
            return listaDeTablero;
        }

        public void ModificarTablero(int idRecibe, Tablero modificarTablero)
        {
            var query = "UPDATE Tablero SET id_usuario_propietario = @idPropietario, nombre_tablero = @nombreTablero, descripcion_tablero = @descripTablero WHERE id_tablero = @idRecibe;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idPropietario", modificarTablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", modificarTablero.NombreDeTablero));
                command.Parameters.Add(new SQLiteParameter("@descripTablero", modificarTablero.DescripcionDeTablero));
                command.Parameters.Add(new SQLiteParameter("@idRecibe", idRecibe));
                int filaAfectada = command.ExecuteNonQuery();
                connection.Close();
                if (filaAfectada == 0)
                {
                    throw new Exception("No se encontrò ningun tablero con el ID solicitado");
                }
            }
        }

        public Tablero ObtenerTableroPorId(int idTablero)
        {
            var query = "SELECT * FROM Tablero WHERE id_tablero = @idTablero;";
            Tablero tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tablero.NombreDeTablero = reader["nombre_tablero"].ToString();
                        tablero.DescripcionDeTablero = reader["descripcion_tablero"].ToString();
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    }
                }
                connection.Close();
            }
            if (tablero == null)
            {
                throw new Exception("El tablero no está creado");

            }
            return tablero;
        }

    }
}
