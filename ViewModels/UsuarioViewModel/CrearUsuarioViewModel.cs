using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.ViewModels;

    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [Display(Name = "Nombre de Usuario")]
        public string? NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [Display(Name = "Contraseña")]
        public string? Contrasenia { get; set; }

        [Required(ErrorMessage = "El rol es requerido.")]
        [Display(Name = "Rol del Usuario")]
        public NivelDeAcceso NivelDeAcceso { get; set; }

        public CrearUsuarioViewModel() { }
        public CrearUsuarioViewModel(Usuario usuario)
        {
            NombreDeUsuario = usuario.NombreDeUsuario!;
            Contrasenia = usuario.Contrasenia!;
            NivelDeAcceso = usuario.Nivel;
        }
    }
