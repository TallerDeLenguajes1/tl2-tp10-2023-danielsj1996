using tl2_tp10_2023_danielsj1996.Models;
namespace tl2_tp10_2023_danielsj1996.ViewModels
{
    public class UsuarioViewModel
    {
        public int IdUsuarioVM { get; set; }
        public string NombreDeUsuarioVM { get; set; }
        public string ContraseniaVM { get; set; }
        public NivelDeAcceso NivelVM { get; set; }

        public UsuarioViewModel() { }

        public UsuarioViewModel(Usuario usuario)
        {

            IdUsuarioVM = usuario.IdUsuario;
            NombreDeUsuarioVM = usuario.NombreDeUsuario;
            ContraseniaVM = usuario.Contrasenia;
            NivelVM = usuario.Nivel;

        }

    }
}