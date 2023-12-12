namespace tl2_tp10_2023_danielsj1996.ViewModels;

using System.ComponentModel.DataAnnotations;

using tl2_tp10_2023_danielsj1996.Models;

public class ListarTableroViewModel{
    private int? id;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }
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
    private EstadoTablero estado;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Estado")]
    public EstadoTablero Estado { get => estado; set => estado = value; }

    public static List<ListarTableroViewModel> FromTablero(List<Tablero> tableros)
    {
        List<ListarTableroViewModel> ListarTableroVM = new List<ListarTableroViewModel>();
        
            foreach (var tablero in tableros)
            {
                ListarTableroViewModel newTVM = new ListarTableroViewModel();
                newTVM.id = tablero.IdTablero;
                newTVM.idUsuarioPropietario = tablero.IdUsuarioPropietario;
                newTVM.nombre = tablero.NombreDeTablero;
                newTVM.Descripcion = tablero.DescripcionDeTablero;
                newTVM.estado = (EstadoTablero)(tl2_tp10_2023_danielsj1996.ViewModels.EstadoTablero)tablero.EstadoTablero;
                ListarTableroVM.Add(newTVM);
            }
            return(ListarTableroVM);
    }
}