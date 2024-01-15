using System.Collections.Generic;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITareaRepository
    {
        public Tarea CrearTarea(Tarea nuevaTarea);
        public List<Tarea> ListarTareas();
        public Tarea ObtenerTareaPorId(int? idTarea); //
        public void EliminarTarea(int? idTarea);
        public Tarea ModificarTarea(Tarea tareaModificada);
        public List<Tarea> ListarTareasDeUsuario(int idUsuario);
        public List<Tarea> ListarTareasDeTablero(int idTablero);
        public void AsignarUsuarioATarea(Tarea tareaModificada);
        public void InhabilitarDeUsuario(int? IdUsuario);

        public void InhabilitarDeTablero(int? IdTablero);
    }
}
