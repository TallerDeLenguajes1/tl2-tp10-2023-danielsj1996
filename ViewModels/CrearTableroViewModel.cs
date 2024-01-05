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
    private string? nombre;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre Tablero")]
    public string? Nombre { get => nombre; set => nombre = value; }

    private string? descripcion;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")]
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    private EstadoTablero estadoTablero;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Estado del tablero")]
    public EstadoTablero EstadoTablero { get => estadoTablero; set => estadoTablero = value; }

    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
       public static CrearTableroViewModel FromTablero(Tablero tablero)
    {

        return new CrearTableroViewModel
        {
            Nombre = tablero.NombreDeTablero,
            Descripcion = tablero.DescripcionDeTablero,
            EstadoTablero = (EstadoTablero)tablero.EstadoTablero,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario,
        };
    }


}