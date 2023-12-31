using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models;
public enum EstadoTarea
{
    Ideas = 1,
    ToDo = 2,
    Doing = 3,
    Review = 4,
    Done = 5,
    Unnactive = 6
}

public class Tarea
{
    private int? idTarea;
    private int? idTablero;  // Agregar el campo idTablero
    private string? nombreTarea;
    private string? descripcionTarea;
    private string? color;
    private EstadoTarea estadoTarea;
    private int? idUsuarioAsignado;
    private int? idUsuarioPropietario;


    public int? IdTarea { get => idTarea; set => idTarea = value; }
    public int? IdTablero { get => idTablero; set => idTablero = value; } // Propiedad para el idTablero
    public string? NombreTarea { get => nombreTarea; set => nombreTarea = value; }

    public EstadoTarea EstadoTarea { get => estadoTarea; set => estadoTarea = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public string? DescripcionTarea { get => descripcionTarea; set => descripcionTarea = value; }
    public string? Color { get => color; set => color = value; }
    public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public Tarea() { }

    public Tarea(int? idTarea, int? idTablero, string? nombreTarea, EstadoTarea estadoTarea, int? idUsuarioAsignado, string? descripcionTarea, string? color, int? idUsuarioPropietario)
    {
        IdTarea = idTarea;
        IdTablero = idTablero;
        NombreTarea = nombreTarea;
        EstadoTarea = estadoTarea;
        IdUsuarioAsignado = idUsuarioAsignado;
        DescripcionTarea = descripcionTarea;
        Color = color;
        IdUsuarioPropietario = idUsuarioPropietario;
    }

    public static Tarea FromCrearTareaViewModel(CrearTareaViewModel tareaVM)
    {

        return new Tarea
        {
            idTablero = tareaVM.IdTablero,
            nombreTarea = tareaVM.Nombre,
            descripcionTarea = tareaVM.Descripcion,
            color = tareaVM.Color,
            estadoTarea = (tl2_tp10_2023_danielsj1996.Models.EstadoTarea)tareaVM.Estado,
            idUsuarioAsignado = tareaVM.IdUsuarioAsignado,
            idUsuarioPropietario = tareaVM.IdUsuarioPropietario,
        };
    }
    public static Tarea FromEditarTareaViewModel(EditarTareaViewModel tareaVM)
    {

        return new Tarea
        {
            
            idTarea = tareaVM.IdTarea,
            idTablero = tareaVM.IdTablero,
            nombreTarea = tareaVM.Nombre,
            descripcionTarea = tareaVM.Descripcion,
            color = tareaVM.Color,
            estadoTarea = (tl2_tp10_2023_danielsj1996.Models.EstadoTarea)tareaVM.Estado,
            idUsuarioAsignado = tareaVM.IdUsuarioAsignado,
            idUsuarioPropietario = tareaVM.IdUsuarioPropietario,
        };
    }
    public static Tarea FromAsignarTareaViewModel(AsignarTareaViewModel tareaVM)
    {

        return new Tarea
        {
            idTarea = tareaVM.Id,
            idTablero = tareaVM.IdTablero,
            nombreTarea = tareaVM.Nombre,
            descripcionTarea = tareaVM.Descripcion,
            color = tareaVM.Color,
            estadoTarea = (tl2_tp10_2023_danielsj1996.Models.EstadoTarea)tareaVM.Estado,
            idUsuarioAsignado = tareaVM.IdUsuarioAsignado,
            idUsuarioPropietario = tareaVM.IdUsuarioPropietario,
        };
    }


}




