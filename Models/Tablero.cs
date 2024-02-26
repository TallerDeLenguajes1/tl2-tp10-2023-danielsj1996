using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models
{

    public class Tablero
    {
        private int idTablero;
        private int idUsuarioPropietario;
        private string? nombreDeTablero;
        private string? descripcionDeTablero;
        private string? nombreDePropietario;




        public int IdTablero { get => idTablero; set => idTablero = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string? NombreDeTablero { get => nombreDeTablero; set => nombreDeTablero = value; }
        public string? DescripcionDeTablero { get => descripcionDeTablero; set => descripcionDeTablero = value; }
        public string? NombreDePropietario { get => nombreDePropietario; set => nombreDePropietario = value; }

        public Tablero() { }

        public Tablero(TableroViewModel tableroViewModel)
        {
            IdTablero = tableroViewModel.IdTableroVM;
            IdUsuarioPropietario = tableroViewModel.IdUsuarioPropietarioVM;
            NombreDeTablero = tableroViewModel.NombreTableroVM;
            DescripcionDeTablero = tableroViewModel.DescripcionVM;
            NombreDePropietario=tableroViewModel.NombrePropietarioVM;
        }
    }
}