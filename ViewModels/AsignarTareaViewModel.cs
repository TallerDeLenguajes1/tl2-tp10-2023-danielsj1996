using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;

public class AsignarTareaViewModel
{
    private int? id;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }

    private int? idTablero;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Id Tablero")]
    public int? IdTablero { get => idTablero; set => idTablero = value; }

    public string? Nombre { get => nombre; set => nombre = value; }
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Nombre")]
    private string? nombre;
    private EstadoTarea estado;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Estado")]
    public EstadoTarea Estado { get => estado; set => estado = value; }
    private string? descripcion;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Descripcion")]
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    private string? color;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "color")]
    public string? Color { get => color; set => color = value; }
    private int? idUsuarioAsignado;
    [Required(ErrorMessage = "Este Campo es Requerido")]
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }


    public static AsignarTareaViewModel FromTarea(Tarea NuevaTarea)
    {

        AsignarTareaViewModel newTVM = new AsignarTareaViewModel();
        newTVM.id = NuevaTarea.IdTarea;
        newTVM.idTablero = NuevaTarea.IdTablero;
        newTVM.Nombre = NuevaTarea.NombreTarea;
        newTVM.estado = (tl2_tp10_2023_danielsj1996.ViewModels.EstadoTarea)NuevaTarea.EstadoTarea;
        newTVM.descripcion = NuevaTarea.DescripcionTarea;
        newTVM.IdUsuarioAsignado = NuevaTarea.IdUsuarioAsignado;
        return (newTVM);

}










}



