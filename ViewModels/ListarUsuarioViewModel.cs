using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Models;

public class ListarUsuarioViewModel
{
    private int? id;
    [Required(ErrorMessage = "Este Campo es requerido.")]
    [Display(Name = "Id")]
    public int? Id { get => id; set => id = value; }

    private string? nombre;
    [Required(ErrorMessage = "Este Campo es Requerido.")]
    [Display(Name = "Nombre")]
    public String? Nombre { get => nombre; set => nombre = value; }
    private string? contrasenia;
    [Required(ErrorMessage = "Este Campo es Requerido.")]
    [Display(Name = "Nombre")]
    public String? Contrasenia { get => contrasenia; set => contrasenia = value; }
    private int nivel;
    [Required(ErrorMessage = "Este Campo es Requerido.")]
    [Display(Name = "Nombre")]
    public int Nivel { get => nivel; set => nivel = value; }

    public static List<ListarUsuarioViewModel> FromUsuario(List<Usuario> usuarios)
    {
        List<ListarUsuarioViewModel> listaUsuariosVM = new List<ListarUsuarioViewModel>();
        foreach (var usuario in usuarios)
        {
            ListarUsuarioViewModel nuevoUVM = new ListarUsuarioViewModel();
            nuevoUVM.Id = usuario.IdUsuario;
            nuevoUVM.Nombre = usuario.NombreDeUsuario;
            nuevoUVM.Contrasenia=usuario.Contrasenia;
            nuevoUVM.Nivel=usuario.Nivel;
            listaUsuariosVM.Add(nuevoUVM);
        }
        return listaUsuariosVM;

    }

}