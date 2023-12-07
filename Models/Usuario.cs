using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models
{

    public class Usuario
    {
        private int? idUsuario;
        private string? nombreDeUsuario;


        public int? IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public Usuario(int? idUsuario, string? nombreDeUsuario)
        {
            this.idUsuario = idUsuario;
            this.nombreDeUsuario = nombreDeUsuario;
        }


        public Usuario() { }
        public static Usuario FromCrearUsuarioViewModel(CrearUsuarioViewModel usuarioVM)
        {
            return new Usuario
            {
                nombreDeUsuario = usuarioVM.Nombre,
                idUsuario = usuarioVM.Id

            };
        }
        public static Usuario FromEditarUsuarioViewModel(EditarUsuarioViewModel usuarioVM)
        {
            return new Usuario
            {
                nombreDeUsuario = usuarioVM.Nombre,
                idUsuario = usuarioVM.Id

            };
        }
    }
}