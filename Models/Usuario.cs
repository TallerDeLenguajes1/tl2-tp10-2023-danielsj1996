using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models
{

    public class Usuario
    {
        private int? idUsuario;
        private string? nombreDeUsuario;
        private string? contrasenia;
        private int nivel;


        public int? IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string? NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string? Contrasenia { get => contrasenia; set => contrasenia = value; }
        public int Nivel { get => nivel; set => nivel = value; }

        public Usuario() { }

        public Usuario(int? idUsuario, string? nombreDeUsuario, string? contrasenia, int nivel)
        {
            this.idUsuario = idUsuario;
            this.nombreDeUsuario = nombreDeUsuario;
            this.contrasenia = contrasenia;
            this.nivel = nivel;
        }

        public static Usuario FromUsuarioViewModel(UsuarioViewModel usuarioVM)
        {
            return new Usuario
            {
                nombreDeUsuario = usuarioVM.Nombre,
                idUsuario = usuarioVM.Id,
                contrasenia = usuarioVM.Contrasenia,
                nivel = usuarioVM.Nivel,
            };
        }
    }
}