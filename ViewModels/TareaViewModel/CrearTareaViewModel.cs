using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.ViewModels
{
    public class CrearTareaViewModel
    {
        [Required(ErrorMessage = "El nombre de la tarea es requerido.")]
        [Display(Name = "Nombre de la Tarea")]
        public string? NombreTarea { get; set; }

        [Display(Name = "Descripción de la Tarea")]
        public string? DescripcionTarea { get; set; }

        [Required(ErrorMessage = "El estado de la tarea es requerido.")]
        [Display(Name = "Estado de la Tarea")]
        public EstadoTarea EstadoTarea { get; set; }

        [Required(ErrorMessage = "El color de la tarea es requerido.")]
        [Display(Name = "Color de la Tarea")]
        public string? ColorTarea { get; set; }

        [Required(ErrorMessage = "El Tablero es requerido.")]
        [Display(Name = "Tablero para la Tarea")]
        public int IdTablero { get; set; }
        public List<Tablero>? ListadoTableros { get; set; }

        [Display(Name = "Tablero para la Tarea")]
        public int? IdUsuarioAsignado { get; set; }
        public List<Usuario>? ListadoUsuariosDisponibles { get; set; }



        public CrearTareaViewModel(Tarea tarea)
        {
            NombreTarea = tarea.NombreTarea;
            DescripcionTarea = tarea.DescripcionTarea;
            EstadoTarea = tarea.EstadoTarea;
            ColorTarea = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado!; // O el valor por defecto que desees
            IdTablero = tarea.IdTablero;
        }

        public CrearTareaViewModel()
        {
        }
    }
}
