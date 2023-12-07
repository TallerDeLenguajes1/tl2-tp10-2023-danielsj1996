using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models
{
    public class Tablero
    {
        private int? idTablero;
        private int? idUsuarioPropietario;
        private string? nombreDeTablero;
        private string? descripcionDeTablero;


        public int? IdTablero { get => idTablero; set => idTablero = value; }
        public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string? NombreDeTablero { get => nombreDeTablero; set => nombreDeTablero = value; }
        public string? DescripcionDeTablero { get => descripcionDeTablero; set => descripcionDeTablero = value; }
        public Tablero(int? idTablero, int? idUsuarioPropietario, string? nombreDeTablero, string? descripcionDeTablero)
        {
            this.idTablero = idTablero;
            this.idUsuarioPropietario = idUsuarioPropietario;
            this.nombreDeTablero = nombreDeTablero;
            this.descripcionDeTablero = descripcionDeTablero;
        }
        public Tablero() { }

        public static Tablero FromCrearTableroViewModel(CrearTableroViewModel tableroVM)
        {
            return new Tablero
            {
                idTablero = tableroVM.Id,
                idUsuarioPropietario=tableroVM.IdUsuarioPropietario,
                nombreDeTablero=tableroVM.Nombre,
                descripcionDeTablero=tableroVM.Descripcion
            };

        }
        public static Tablero FromEditarTableroViewModel(EditarTableroViewModel tableroVM)
        {
            return new Tablero
            {
                idTablero = tableroVM.Id,
                idUsuarioPropietario=tableroVM.IdUsuarioPropietario,
                nombreDeTablero=tableroVM.Nombre,
                descripcionDeTablero=tableroVM.Descripcion
            };

        }
    }
}