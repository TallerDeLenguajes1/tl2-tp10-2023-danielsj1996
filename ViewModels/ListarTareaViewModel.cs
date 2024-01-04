using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;

public class ListarTareaViewModel
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


    public static List<ListarTareaViewModel> FromTarea(List<Tarea> tareas)
    {
        List<ListarTareaViewModel> listadeTareasVM = new List<ListarTareaViewModel>();

        foreach (var tarea in tareas)
        {
            ListarTareaViewModel newTVM = new ListarTareaViewModel();
            newTVM.id = tarea.IdTarea;
            newTVM.idTablero = tarea.IdTablero;
            newTVM.nombre = tarea.NombreTarea;
            newTVM.estado = tarea.EstadoTarea;
            newTVM.descripcion = tarea.DescripcionTarea;
            newTVM.color = tarea.Color;
            newTVM.idUsuarioAsignado = tarea.IdUsuarioAsignado;
            newTVM.idUsuarioPropietario = tarea.IdUsuarioPropietario;
            listadeTareasVM.Add(newTVM);
        }
        return listadeTareasVM;
    }

}