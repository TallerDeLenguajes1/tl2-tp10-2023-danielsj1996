namespace tl2_tp10_2023_danielsj1996.Models
{
    public enum RolUsuario
    {
        Administrador = 1,
        General=2

    }
    public class Usuario
    {
        private string nombreDeUsuario;
        private int idUsuario;

private RolUsuario rol;
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public RolUsuario Rol { get => rol; set => rol = value; }
    }
}