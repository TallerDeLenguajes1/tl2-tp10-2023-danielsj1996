using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;

public enum EstadoTarea
{
    Ideas = 1,
    ToDo = 2,
    Doing = 3,
    Review = 4,
    Done = 5,
    Unnactive = 6
}

public class TareaViewModel
{
    private int? id;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }
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
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    private int? idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }

    public static TareaViewModel FromTarea(Tarea tareas)
    {
        TareaViewModel newTVM = new TareaViewModel();
        newTVM.id = tareas.IdTarea;
        newTVM.idTablero = tareas.IdTablero;
        newTVM.nombre = tareas.NombreTarea;
        newTVM.estado = (tl2_tp10_2023_danielsj1996.ViewModels.EstadoTarea)tareas.EstadoTarea;
        newTVM.descripcion = tareas.DescripcionTarea;
        newTVM.color = tareas.Color;
        newTVM.idUsuarioAsignado = tareas.IdUsuarioAsignado;
        newTVM.idUsuarioPropietario = tareas.IdUsuarioPropietario;

        return (newTVM);
    }
    public static List<TareaViewModel> FromTarea(List<Tarea> tareas)
    {
        List<TareaViewModel> listadeTareasVM = new List<TareaViewModel>();

        foreach (var tarea in tareas)
        {
            TareaViewModel newTVM = new TareaViewModel();
            newTVM.id = tarea.IdTarea;
            newTVM.idTablero = tarea.IdTablero;
            newTVM.nombre = tarea.NombreTarea;
            newTVM.estado = (tl2_tp10_2023_danielsj1996.ViewModels.EstadoTarea)tarea.EstadoTarea;
            newTVM.descripcion = tarea.DescripcionTarea;
            newTVM.color = tarea.Color;
            newTVM.idUsuarioAsignado = tarea.IdUsuarioAsignado;
            newTVM.idUsuarioPropietario = tarea.IdUsuarioPropietario;
            listadeTareasVM.Add(newTVM);
        }
        return listadeTareasVM;
    }

}