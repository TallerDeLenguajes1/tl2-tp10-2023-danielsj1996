namespace tl2_tp10_2023_danielsj1996.ViewModels;

using System.ComponentModel.DataAnnotations;

using tl2_tp10_2023_danielsj1996.Models;

public class EditarTableroViewModel{
    private int id;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }
    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Propietario")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
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
    [Display(Name = "Estado")]
    public EstadoTablero EstadoTablero { get => estadoTablero; set => estadoTablero = value; }

    public static EditarTableroViewModel FromTablero(Tablero tablero)
    {
        return new EditarTableroViewModel
        {
            id = tablero.IdTablero,
            idUsuarioPropietario = tablero.IdUsuarioPropietario,
            nombre = tablero.NombreDeTablero,
            descripcion=tablero.DescripcionDeTablero,
            estadoTablero = (EstadoTablero)tablero.EstadoTablero
        };
    }
}