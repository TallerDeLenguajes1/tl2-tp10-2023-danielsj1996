using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;


public class EditarTareaViewModel
{
    private int? idTarea;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int? IdTarea { get => idTarea; set => idTarea = value; }
    private int? idTablero;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Tablero")]
    public int? IdTablero { get => idTablero; set => idTablero = value; }
    private string? nombre;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre Tarea")]
    public string? Nombre { get => nombre; set => nombre = value; }
    private EstadoTarea estado;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Estado")]
    public EstadoTarea Estado { get => estado; set => estado = value; }
    private string? descripcion;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")]
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    private string? color;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Color")]
    public string? Color { get => color; set => color = value; }
    private int? idUsuarioAsignado;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nuevo Usuario Asignado a la tarea")]
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nuevo Usuario Propietario de la Tarea")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    

    public static EditarTareaViewModel FromTarea(Tarea tareas)
    {
        EditarTareaViewModel newTVM = new EditarTareaViewModel();
        newTVM.idTarea = tareas.IdTarea;
        newTVM.idTablero = tareas.IdTablero;
        newTVM.nombre = tareas.NombreTarea;
        newTVM.estado = (EstadoTarea)tareas.EstadoTarea;
        newTVM.descripcion = tareas.DescripcionTarea;
        newTVM.color = tareas.Color;
        newTVM.idUsuarioAsignado = tareas.IdUsuarioAsignado;
        newTVM.idUsuarioPropietario = tareas.IdUsuarioPropietario;
        return newTVM;
    }

}