using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models;
public enum EstadoTarea
{
    Ideas = 1,
    Pendiente = 2,
    EnProceso = 3,
    Revisar = 4,
    Realizada = 5,
    
}

    public class Tarea
    {
        public int IdTarea { get; set; }
        public int IdTablero { get; set; }
        public string? NombreTarea { get; set; }
        public EstadoTarea EstadoTarea { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public string? DescripcionTarea { get; set; }
        public string? Color { get; set; }
        public string? NombreUsuarioAsignado { get; set; }
        public string? NombreDelTableroPertenece { get; set; }

        public Tarea() { }

        public Tarea(TareaViewModel tareaViewModel)
        {
            IdTarea = tareaViewModel.IdTareaVM;
            IdTablero = tareaViewModel.IdTableroVM;
            NombreTarea = tareaViewModel.NombreTareaVM;
            EstadoTarea = tareaViewModel.EstadoTareaVM;
            IdUsuarioAsignado = tareaViewModel.IdUsuarioAsignadoVM;
            DescripcionTarea = tareaViewModel.DescripcionTareaVM;
            Color = tareaViewModel.ColorVM;
            NombreUsuarioAsignado = tareaViewModel.NombreUsuarioAsignadoVM;
            NombreDelTableroPertenece = tareaViewModel.NombreDelTableroPerteneceVM;
        }
    }





