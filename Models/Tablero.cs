using System.Data.SQLite;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Models
{

    public enum EstadoTablero
    {
        Active = 1,
        Unnactive = 2
    }
    public class Tablero
    {
        private int? idTablero;
        private int? idUsuarioPropietario;
        private string? nombreDeTablero;
        private string? descripcionDeTablero;
        private EstadoTablero estadoTablero;
        private List<Usuario> listadeUsuarios;


        public int? IdTablero { get => idTablero; set => idTablero = value; }
        public int? IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string? NombreDeTablero { get => nombreDeTablero; set => nombreDeTablero = value; }
        public string? DescripcionDeTablero { get => descripcionDeTablero; set => descripcionDeTablero = value; }
        public EstadoTablero EstadoTablero { get => estadoTablero; set => estadoTablero = value; }
        public List<Usuario> ListadeUsuarios { get => listadeUsuarios; set => listadeUsuarios = value; }

        public Tablero() { }

        public Tablero(int? idTablero, int? idUsuarioPropietario, string? nombreDeTablero, string? descripcionDeTablero, EstadoTablero estadoTablero, List<Usuario> listadeUsuarios)
        {
            IdTablero = idTablero;
            IdUsuarioPropietario = idUsuarioPropietario;
            NombreDeTablero = nombreDeTablero;
            DescripcionDeTablero = descripcionDeTablero;
            EstadoTablero = estadoTablero;
            ListadeUsuarios = listadeUsuarios;
        }

        public static Tablero FromCrearTableroViewModel(CrearTableroViewModel tableroVM)
        {
            return new Tablero
            {
                idTablero = tableroVM.Id,
                idUsuarioPropietario = tableroVM.IdUsuarioPropietario,
                nombreDeTablero = tableroVM.Nombre,
                descripcionDeTablero = tableroVM.Descripcion,
                estadoTablero = (tl2_tp10_2023_danielsj1996.Models.EstadoTablero)tableroVM.EstadoTablero
            };

        }
        public static Tablero FromEditarTableroViewModel(EditarTableroViewModel tableroVM)
        {
            return new Tablero
            {
                idTablero = tableroVM.Id,
                idUsuarioPropietario = tableroVM.IdUsuarioPropietario,
                nombreDeTablero = tableroVM.Nombre,
                descripcionDeTablero = tableroVM.Descripcion,
                estadoTablero = (tl2_tp10_2023_danielsj1996.Models.EstadoTablero)tableroVM.EstadoTablero
            };

        }



    }
}