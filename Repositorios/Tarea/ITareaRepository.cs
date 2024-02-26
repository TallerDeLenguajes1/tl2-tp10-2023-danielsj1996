using System.Collections.Generic;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITareaRepository
    {
        void CrearTarea(int idTablero, Tarea nuevaTarea);
        void ModificarTarea(int idTarea, Tarea tareaModificada);
        void EliminarTarea(int idTarea);
        List<Tarea> ListarTodasLasTareas();
        Tarea ObtenerTareaPorId(int idTarea);
        void CambiarPropietarioTarea(Tarea tarea);
        List<Tarea> ListarTareasDeUsuario(int idUsuario);
        List<Tarea> ListarTareasDeTablero(int idTablero);
        List<Tarea> BuscarTareasPorNombre(string nombre);
    }
}
