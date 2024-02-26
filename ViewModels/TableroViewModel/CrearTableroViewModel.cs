using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;


using tl2_tp10_2023_danielsj1996.Models;
public enum EstadoTablero
{
    Active = 1,
    Unnactive = 2
}
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage = "El nombre del tablero es requerido.")]
        [Display(Name = "Nombre del Tablero")]
        public string? NombreDeTablero { get; set; }

        [Display(Name = "Descripción del Tablero")]
        public string? DescripcionDeTablero { get; set; }

        [Required(ErrorMessage = "El propietario del tablero es requerido.")]
        [Display(Name = "Nombre del propietario del Tablero")]
        public int IdUsuarioPropietario { get; set; }
        public List<Usuario>? ListadoUsuarios { get; set; }

        public CrearTableroViewModel() { }
        public CrearTableroViewModel(Tablero tablero)
        {
            NombreDeTablero = tablero.NombreDeTablero;
            DescripcionDeTablero = tablero.DescripcionDeTablero;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        }
    }
