using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios;

public class TareaRepository : ITareaRepository
{
    private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

    public Tarea CrearTarea(Tarea nuevaTarea)
    {
        var query = "INSERT INTO Tarea (id_tablero, nombre_tarea, descripcion_tarea, estado_tarea, color_tarea, id_usuario_asignado) " +
                    "VALUES (@idTablero, @nombreTarea, @descripcionTarea, @estadoTarea, @colorTarea, @idUsuarioA);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", nuevaTarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombreTarea", nuevaTarea.NombreTarea));
            command.Parameters.Add(new SQLiteParameter("@descripcionTarea", nuevaTarea.DescripcionTarea));
            command.Parameters.Add(new SQLiteParameter("@estadoTarea", nuevaTarea.EstadoTarea.ToString()));
            command.Parameters.Add(new SQLiteParameter("@colorTarea", nuevaTarea.Color));
            command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", nuevaTarea.IdUsuarioAsignado));
            command.ExecuteNonQuery();
            connection.Close();
        }
        if (nuevaTarea == null)
        {
            throw new Exception("La Tarea no pudo ser Creada correctamente");
        }
        else
        {
            return nuevaTarea;

        }
    }
    public List<Tarea> ListarTareas()
    {
        List<Tarea> tareas = new List<Tarea>();
        SQLiteConnection connectionC = new SQLiteConnection(cadenaConexion);

        string queryC = @"SELECT * FROM Tarea;";

        using (connectionC)
        {
            connectionC.Open();
            SQLiteCommand commandC = new SQLiteCommand(queryC, connectionC);

            SQLiteDataReader readerC = commandC.ExecuteReader();
            using (readerC)
            {
                while (readerC.Read())
                {
                    var newTarea = new Tarea();
                    newTarea.IdTarea = Convert.ToInt32(readerC["id_tarea"]);
                    newTarea.IdTablero = Convert.ToInt32(readerC["id_tablero"]);
                    newTarea.NombreTarea = Convert.ToString(readerC["nombre_tarea"]);
                    newTarea.EstadoTarea = (EstadoTarea)Convert.ToInt32(readerC["estado_tarea"]);
                    newTarea.DescripcionTarea = Convert.ToString(readerC["descripcion_tarea"]);
                    newTarea.Color = Convert.ToString(readerC["color_tarea"]);
                    newTarea.IdUsuarioAsignado = Convert.ToInt32(readerC["id_usuario_asignado"]);
                    tareas.Add(newTarea);
                }
            }
            connectionC.Close();
        }
        if (tareas == null)
        {
            throw new Exception("Lista de tareas no encontrada.");
        }
        return tareas;
    }
    public Tarea ObtenerTareaPorId(int? idTarea)
    {
        var query = "SELECT * FROM Tarea WHERE id_tarea = @idTarea;";
        Tarea tarea = new Tarea();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.IdTarea = Convert.ToInt32(reader["id_tarea"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.NombreTarea = reader["nombre_tarea"].ToString();
                    tarea.DescripcionTarea = reader["descripcion_tarea"].ToString();
                    tarea.Color = reader["color_tarea"].ToString();
                    tarea.EstadoTarea = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado_tarea"].ToString());
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                }
            }
            connection.Close();
        }
        if (tarea == null)
        {
            throw new Exception("La tarea no està creada");
        }
        else
        {
            return tarea;
        }
    }
    public void EliminarTarea(int? idTarea)
    {
        var query = "DELETE FROM Tarea WHERE id_tarea = @idTarea;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            int filaAfectada = command.ExecuteNonQuery();
            connection.Close();
            if (filaAfectada == 0)
            {
                throw new Exception("No se encontrò ninguna Tarea con el ID solicitado");
            }
        }

    }

    public Tarea ModificarTarea(Tarea tareaModificada)
    {
        var query = "UPDATE Tarea " +
                    "SET nombre_tarea = @nombreTarea, descripcion_tarea = @descripcionTarea, estado_tarea = @estadoTarea, color_tarea = @colorTarea, id_usuario_asignado = @idUsuarioAsignado " +
                    "WHERE id_tarea = @idTarea;";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", tareaModificada.IdTarea));
            command.Parameters.Add(new SQLiteParameter("@nombreTarea", tareaModificada.NombreTarea));
            command.Parameters.Add(new SQLiteParameter("@descripcionTarea", tareaModificada.DescripcionTarea));
            command.Parameters.Add(new SQLiteParameter("@estadoTarea", tareaModificada.EstadoTarea.ToString()));
            command.Parameters.Add(new SQLiteParameter("@colorTarea", tareaModificada.Color));
            command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", tareaModificada.IdUsuarioAsignado));

            int filaAfectada = command.ExecuteNonQuery();
            connection.Close();
            if (filaAfectada == 0)
            {
                throw new Exception("No se encontró ninguna tarea con el ID proporcionado");
            }

        }

        return tareaModificada;
    }





    public List<Tarea> ListarTareasDeUsuario(int? idUsuario)
    {
        var query = "SELECT * FROM Tarea WHERE id_usuario_asignado = @id_usuario";
        List<Tarea> listaDeTareas = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id_usuario", idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea();
                    tarea.IdTarea = Convert.ToInt32(reader["id_tarea"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.NombreTarea = reader["nombre_tarea"].ToString();
                    tarea.DescripcionTarea = reader["descripcion_tarea"].ToString();
                    tarea.Color = reader["color_tarea"].ToString();
                    tarea.EstadoTarea = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado_tarea"].ToString());
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    listaDeTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        if (listaDeTareas == null)
        {
            throw new Exception("El usuario proporcionado no tiene Tareas asignadas");
        }
        else
        {
            return listaDeTareas;
        }
    }

    public List<Tarea> ListarTareasDeTablero(int? idTablero)
    {
        var query = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";
        List<Tarea> listaDeTareas = new List<Tarea>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tarea = new Tarea();
                    tarea.IdTarea = Convert.ToInt32(reader["id_tarea"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.NombreTarea = reader["nombre_tarea"].ToString();
                    tarea.DescripcionTarea = reader["descripcion_tarea"].ToString();
                    tarea.Color = reader["color_tarea"].ToString();
                    tarea.EstadoTarea = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), reader["estado_tarea"].ToString());
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    listaDeTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        if (listaDeTareas == null)
        {
            throw new Exception("El tablero proporcionado no tiene tareas asignadas");

        }
        else
        {
            return listaDeTareas;

        }

    }
    public void AsignarUsuarioATarea(Tarea tareaModificada)
    {
        var query = "UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id_tarea = @idTarea";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", tareaModificada.IdUsuarioAsignado);
                command.Parameters.AddWithValue("@idTarea", tareaModificada.IdTarea);
                int filaAfectada = command.ExecuteNonQuery();
                connection.Close();
                if (filaAfectada == 0)
                {
                    throw new Exception("No se encontro ninguna Tarea con el ID proporcionado");
                }
            }
        }
    }





}

