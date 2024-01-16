using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public class TableroRepository : ITableroRepository{
    private readonly string CadenaConexion;

    public TableroRepository(string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }

    

    public Tablero CrearTablero(Tablero nuevoTablero)
    {
        var query = "INSERT INTO Tablero (nombre_tablero, descripcion_tablero, estado_tablero,id_usuario_propietario) VALUES (@nombreTablero, @descripcionTablero, @estadoTablero,@idUsuarioProp);";
        Console.WriteLine("Consulta SQL: " + query);
        using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
        {
            try
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", nuevoTablero.NombreDeTablero));
                command.Parameters.Add(new SQLiteParameter("@descripcionTablero", nuevoTablero.DescripcionDeTablero));
                command.Parameters.Add(new SQLiteParameter("@estadoTablero", nuevoTablero.EstadoTablero));
                command.Parameters.Add(new SQLiteParameter("@idUsuarioProp", nuevoTablero.IdUsuarioPropietario));
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                throw;
            }

        }
        if (nuevoTablero == null)
        {
            throw new Exception("La Tarea no pudo ser Creada correctamente");
        }
        else
        {
            return nuevoTablero;

        }
    }

        public List<Tablero> ListarTodosTableros()
        {
            List<Tablero> listaDeTablero = new List<Tablero>();
            var query = "SELECT * FROM Tablero";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
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
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado_tablero"]);
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

        public Tablero ObtenerTableroPorId(int? idTablero)
        {
            var query = "SELECT * FROM Tablero WHERE id_tablero = @idTablero;";
            Tablero tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
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
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado_tablero"]);
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
        public void EliminarTableroPorId(int IdTablero)
        {
        TareaRepository repoT = new TareaRepository(CadenaConexion);
        repoT.InhabilitarDeTablero(IdTablero);

            var query = "DELETE FROM Tablero WHERE id_tablero = @idRecibe";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idRecibe", IdTablero));
                int filaAfectada = command.ExecuteNonQuery();
                connection.Close();
                if (filaAfectada == 0)
                {
                    throw new Exception("No se encontró ningun tablero con el ID proporcionado");
                }
            }
        }
        public Tablero ModificarTablero(Tablero modificarTablero)
        {
            var query = "UPDATE Tablero SET id_usuario_propietario = @idPropietario, nombre_tablero = @nombreTablero, descripcion_tablero = @descripTablero,estado_tablero = @estado WHERE id_tablero = @idTablero;";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", modificarTablero.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@idPropietario", modificarTablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", modificarTablero.NombreDeTablero));
                command.Parameters.Add(new SQLiteParameter("@descripTablero", modificarTablero.DescripcionDeTablero));
                command.Parameters.Add(new SQLiteParameter("@estado", modificarTablero.EstadoTablero));
                int filaAfectada = command.ExecuteNonQuery();
                connection.Close();
                if (filaAfectada == 0)
                {
                    throw new Exception("No se encontrò ningun tablero con el ID solicitado");
                }
            }
            return modificarTablero;
        }
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int idUsuario)
        {
            var query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
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
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.NombreDeTablero = reader["nombre_tablero"].ToString();
                        tablero.DescripcionDeTablero = reader["descripcion_tablero"].ToString();
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado_tablero"]);
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
       public void InhabilitarDeUsuario(int IdUsuario){
        try{// se uso try-catch para poder lanzar la excepcion sin que se detenga el proceso ya que puede existir usuarios sin tableros
            TareaRepository repoT = new TareaRepository(CadenaConexion);
            repoT.InhabilitarDeUsuario(IdUsuario);

            SQLiteConnection connectionC = new SQLiteConnection(CadenaConexion);
            
            string queryC = "UPDATE Tablero SET estado_tablero = @ESTADO WHERE id_usuario_propietario = @ID";
            SQLiteParameter parameterId = new SQLiteParameter("@ID",IdUsuario);
            SQLiteParameter parameterEstado = new SQLiteParameter("@ESTADO",2);

            using (connectionC)
            {
                connectionC.Open();
                SQLiteCommand commandC = new SQLiteCommand(queryC,connectionC);
                commandC.Parameters.Add(parameterId);
                commandC.Parameters.Add(parameterEstado);

                int rowAffected =  commandC.ExecuteNonQuery();
                connectionC.Close();
                if (rowAffected == 0){
                    throw new Exception("No hay tableros para inhabilitar.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción: {ex.Message}");
        }
    }
    }

}

