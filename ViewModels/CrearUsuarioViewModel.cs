using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;
public class CrearUsuarioViewModel
{
    private int? id;
    [Required(ErrorMessage = "Este Campo es Requerido.")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }
    private string? nombre;
    [Required(ErrorMessage = "Este Campo es Requerido.")]
    [Display(Name = "Nombre")]
    public string? Nombre { get => nombre; set => nombre = value; }

   }