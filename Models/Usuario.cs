namespace tl2_tp10_2023_danielsj1996.Models;

using tl2_tp10_2023_danielsj1996.ViewModels;

public class Usuario{
    private int? idUsuario;
    private string? nombreDeUsuario;
    private string contrasenia;
    private int nivel;
    

    public Usuario(){

    }

    public Usuario(int? idUsuario, string? nombreDeUsuario, string contrasenia, int nivel)
    {
        this.idUsuario = idUsuario;
        this.nombreDeUsuario = nombreDeUsuario;
        this.contrasenia = contrasenia;
        this.nivel = nivel;
    }

    public int? IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public int Nivel { get => nivel; set => nivel = value; }

    public static Usuario FromCrearUsuarioViewModel(CrearUsuarioViewModel usuarioVM)
    {
        return new Usuario
        {
            nombreDeUsuario = usuarioVM.Nombre,
            Contrasenia = usuarioVM.Contrasenia,
            Nivel = usuarioVM.Nivel
        };
    }
    public static Usuario FromEditarUsuarioViewModel(EditarUsuarioViewModel usuarioVM)
    {
        return new Usuario
        {
            idUsuario = usuarioVM.Id,
            nombreDeUsuario = usuarioVM.Nombre,
            Contrasenia = usuarioVM.Contrasenia,
            Nivel = usuarioVM.Nivel
        };
    }
    
}