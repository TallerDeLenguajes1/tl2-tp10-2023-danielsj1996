using tl2_tp10_2023_danielsj1996.ViewModels;
namespace tl2_tp10_2023_danielsj1996.Models;
public enum NivelDeAcceso
{
    invitado = 1,
    admin = 2
}


public class Login
{
    private NivelDeAcceso nivel;
    private string? nombre;
    private string contrasenia;

    public NivelDeAcceso Nivel { get => nivel; set => nivel = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public Login() { }

    public Login(LoginViewModel login)
    {

        nombre = login.Nombre;
        contrasenia = login.Contrasenia;
    }



}