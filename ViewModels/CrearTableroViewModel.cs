using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;

using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc.Rendering;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
public enum EstadoTablero
{
    Active = 1,
    Unnactive = 2
}
public class CrearTableroViewModel
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
    


    public static CrearTableroViewModel FromTablero(Tablero tablero)
    {

        return new CrearTableroViewModel
        {
            Id = tablero.IdTablero,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario,
            Nombre = tablero.NombreDeTablero,
            Descripcion = tablero.DescripcionDeTablero,
            EstadoTablero = (EstadoTablero)(tl2_tp10_2023_danielsj1996.ViewModels.EstadoTablero)tablero.EstadoTablero,
        };
    }


}