using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.ViewModels
{
    public class TareaViewModel
    {
        public int IdTareaVM { get; set; }
        public int IdTableroVM { get; set; }
        public string? NombreTareaVM { get; set; }
        public EstadoTarea EstadoTareaVM { get; set; }
        public string? DescripcionTareaVM { get; set; }
        public string? ColorVM { get; set; }
        public int? IdUsuarioAsignadoVM { get; set; }
        public string? NombreUsuarioAsignadoVM { get; set; }
        public string? NombreDelTableroPerteneceVM { get; set; }

        public TareaViewModel() { }
        public TareaViewModel(Tarea tarea)
        {
            IdTareaVM = tarea.IdTarea;
            IdTableroVM = tarea.IdTablero;
            NombreTareaVM = tarea.NombreTarea;
            EstadoTareaVM = tarea.EstadoTarea;
            DescripcionTareaVM = tarea.DescripcionTarea;
            ColorVM = tarea.Color;
            IdUsuarioAsignadoVM = tarea.IdUsuarioAsignado!;
            NombreUsuarioAsignadoVM = tarea.NombreUsuarioAsignado;
            NombreDelTableroPerteneceVM = tarea.NombreDelTableroPertenece;
        }
    }
}