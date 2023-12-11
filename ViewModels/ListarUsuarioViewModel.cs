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
    [Required(ErrorMessage="Este Campo es Requerido.")]
    [Display(Name = "Nombre")]
    public String? Nombre { get => nombre; set => nombre = value; }

    public static List<ListarUsuarioViewModel> FromUsuario(List<Usuario> usuarios)
    {
        List<ListarUsuarioViewModel> listaUsuariosVM = new List<ListarUsuarioViewModel>();
        foreach (var usuario in usuarios)
        {
            ListarUsuarioViewModel nuevoUVM = new ListarUsuarioViewModel();
            nuevoUVM.id = usuario.IdUsuario;
            nuevoUVM.nombre = usuario.NombreDeUsuario;
            listaUsuariosVM.Add(nuevoUVM);
        }
        return listaUsuariosVM;

    }

}