using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;

using tl2_tp10_2023_danielsj1996.Models;

public class EditarUsuarioViewModel
{
    private int? id;

    [Required(ErrorMessage = "Este campo es Requerido")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }
    private string nombre;

    [Required(ErrorMessage = "Este campo es Requerido")]
    [Display(Name = "nombre")]
    public string? Nombre { get => nombre; set => nombre = value; }

    public static EditarUsuarioViewModel FromUsuario(Usuario usuario)
    {
        return new EditarUsuarioViewModel
        {
            nombre = usuario.NombreDeUsuario,
            id = usuario.IdUsuario
        };

    }
}