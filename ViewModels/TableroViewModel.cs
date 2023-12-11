using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;
public enum EstadoTablero{
  Active=1,
  Unnactive=2
}
public class TableroViewModel
{
    private int? id;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }

    private string? nombre;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre Tarea")]
    public string? Nombre { get => nombre; set => nombre = value; }

    private string? descripcion;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")]
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    private EstadoTablero estadoTablero;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")]
    public EstadoTablero EstadoTablero { get => estadoTablero; set => estadoTablero = value; }

    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }


    public static TableroViewModel FromTarea(Tablero tablero)
    {

        return new TableroViewModel
        {
            Id = tablero.IdTablero,
            Nombre = tablero.NombreDeTablero,
            Descripcion = tablero.DescripcionDeTablero,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario,
            EstadoTablero = (tl2_tp10_2023_danielsj1996.ViewModels.EstadoTablero)tablero.EstadoTablero,
        };
    }
    public static List<TableroViewModel> FromTarea(List<Tablero> tableros)
    {
        List<TableroViewModel> listaTablerosVM = new List<TableroViewModel>();

        foreach (var tablero in tableros)
        {
            TableroViewModel newTVM = new TableroViewModel();
            newTVM.id = tablero.IdTablero;
            newTVM.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            newTVM.nombre = tablero.NombreDeTablero;
            newTVM.descripcion = tablero.DescripcionDeTablero;
            newTVM.estadoTablero = (tl2_tp10_2023_danielsj1996.ViewModels.EstadoTablero)tablero.EstadoTablero;
            listaTablerosVM.Add(newTVM);
        }
        return (listaTablerosVM);
    }

}