namespace tl2_tp10_2023_danielsj1996.Models;

using tl2_tp10_2023_danielsj1996.ViewModels;
public enum NivelDeAcceso
{
    admin = 1,
    operador = 2
}
public class Usuario
{
    private int idUsuario;
    private string? nombreDeUsuario;
    private string contrasenia;
    private NivelDeAcceso nivel;

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public NivelDeAcceso Nivel { get => nivel; set => nivel = value; }

    public Usuario() { }

    public Usuario(UsuarioViewModel usuarioViewModel)
    {

        IdUsuario = usuarioViewModel.IdUsuarioVM;
        NombreDeUsuario = usuarioViewModel.NombreDeUsuarioVM;
        Contrasenia = usuarioViewModel.ContraseniaVM;
        Nivel = usuarioViewModel.NivelVM;
    }
}