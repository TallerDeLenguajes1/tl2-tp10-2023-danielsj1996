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

    

        public void CrearTablero(Tablero nuevoTablero)
        {
            var query = "INSERT INTO Tablero (id_tablero,id_usuario_propietario,nombre_tablero,descripcion_tablero,estado_tablero) VALUES (@idTablero,@idPropietario,@nombreTablero,@descripTablero,@estado);";
            using (SQLiteConnection connection = new SQLiteConnection(CadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", nuevoTablero.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@idPropietario", nuevoTablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombreTablero", nuevoTablero.NombreDeTablero));
                command.Parameters.Add(new SQLiteParameter("@descripTablero", nuevoTablero.DescripcionDeTablero));
                command.Parameters.Add(new SQLiteParameter("@estado", nuevoTablero.EstadoTablero));
                command.ExecuteNonQuery();
                connection.Close();
            }
            if (nuevoTablero == null)
            {
                throw new Exception("El Tablero no se pudo crear,revise los datos e intente nuevamente");
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
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado"]);
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
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado"]);
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
        public void EliminarTableroPorId(int? IdTablero)
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
        public void ModificarTablero(Tablero modificarTablero)
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
        }
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int? idUsuario)
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
                        tablero.EstadoTablero = (EstadoTablero)Convert.ToInt32(reader["estado"]);
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
       public void InhabilitarDeUsuario(int? IdUsuario){
        try{// se uso try-catch para poder lanzar la excepcion sin que se detenga el proceso ya que puede existir usuarios sin tableros
            TareaRepository repoT = new TareaRepository(CadenaConexion);
            repoT.InhabilitarDeUsuario(IdUsuario);

            SQLiteConnection connectionC = new SQLiteConnection(CadenaConexion);
            
            string queryC = "UPDATE Tablero SET estado = @ESTADO WHERE id_usuario_propietario = @ID";
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

