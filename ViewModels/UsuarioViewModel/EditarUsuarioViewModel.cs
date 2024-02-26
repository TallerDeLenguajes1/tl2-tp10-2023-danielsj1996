namespace tl2_tp10_2023_danielsj1996.ViewModels;

using System.ComponentModel.DataAnnotations;

using tl2_tp10_2023_danielsj1996.Models;

    public class EditarUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [Display(Name = "Nombre de Usuario")]
        public string? NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [Display(Name = "Contraseña")]
        public string? Contrasenia { get; set; }

        [Required(ErrorMessage = "El rol es requerido.")]
        [Display(Name = "Rol")]
        public NivelDeAcceso NivelDeAcceso { get; set; }

        public EditarUsuarioViewModel() { }
        public EditarUsuarioViewModel(Usuario usuario)
        {
            NombreDeUsuario = usuario.NombreDeUsuario!;
            Contrasenia = usuario.Contrasenia!;
            NivelDeAcceso = usuario.Nivel;
        }
    }

