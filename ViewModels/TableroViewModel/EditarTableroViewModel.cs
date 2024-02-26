namespace tl2_tp10_2023_danielsj1996.ViewModels;

using System.ComponentModel.DataAnnotations;

using tl2_tp10_2023_danielsj1996.Models;

public class EditarTableroViewModel
{
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "El nombre del tablero es requerido.")]
    [Display(Name = "Nombre del Tablero")]
    public string? NombreDeTablero { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string? DescripcionDeTablero { get; set; }

    [Required(ErrorMessage = "El nombre del propietario del tablero es requerido.")]
    [Display(Name = "nombre del propietario del Tablero")]
    public int IdUsuarioPropietario { get; set; }
    public List<Usuario>? ListadoUsuarios { get; set; }

    public EditarTableroViewModel() { }

    public EditarTableroViewModel(Tablero tablero)
    {
        NombreDeTablero = tablero.NombreDeTablero!;
        DescripcionDeTablero = tablero.DescripcionDeTablero;
        IdUsuarioPropietario = (int)tablero.IdUsuarioPropietario!;
    }
}
