using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models;
public enum EstadoTarea
{
    Ideas=1,
    ToDo=2, //hacer
    Doing=3, //haciendo
    Review=4, //revisar
    Done=5, //hecho
    Unnactive=6 //inactivo
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

    public Tarea(int? idTarea, int? idTablero, string? nombreTarea, string? descripcionTarea, string? color, EstadoTarea estadoTarea, int? idUsuarioAsignado, int? idUsuarioPropietario)
    {
        this.idTarea = idTarea;
        this.idTablero = idTablero;
        this.nombreTarea = nombreTarea;
        this.descripcionTarea = descripcionTarea;
        this.color = color;
        this.estadoTarea = estadoTarea;
        this.idUsuarioAsignado = idUsuarioAsignado;
        this.idUsuarioPropietario = idUsuarioPropietario;
    }



    public static Tarea FromTareaViewModel(TareaViewModel tareaVM)
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




