using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.ViewModels
{
    public class TableroViewModel
    {

        public int IdTableroVM { get; set; }
        public int IdUsuarioPropietarioVM { get; set; }
        public string? NombreTableroVM { get; set; }
        public string? DescripcionVM { get; set; }
        public string? NombrePropietarioVM { get; set; }
        public List<Tarea>? ListaDeTareas { get; set; }
        
        
        public TableroViewModel() { }

        public TableroViewModel(Tablero tablero)
        {
            IdTableroVM = tablero.IdTablero;
            IdUsuarioPropietarioVM = tablero.IdUsuarioPropietario;
            NombreTableroVM = tablero.NombreDeTablero;
            DescripcionVM = tablero.DescripcionDeTablero;
            NombrePropietarioVM = tablero.NombreDePropietario;
        }
    }
}