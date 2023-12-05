using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;

public class ListarTableroViewModel{
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

    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    

    public static List<ListarTableroViewModel> FromTarea(List<Tablero> tableros)
    {
        List<ListarTableroViewModel> listaTablerosVM = new List<ListarTableroViewModel>();
        
            foreach (var tablero in tableros)
            {
                ListarTableroViewModel newTVM = new ListarTableroViewModel();
                newTVM.id = tablero.IdTablero;
                newTVM.nombre = tablero.NombreDeTablero;
                newTVM.descripcion = tablero.DescripcionDeTablero;
                newTVM.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
                listaTablerosVM.Add(newTVM);
            }
            return(listaTablerosVM);
    }
    
}