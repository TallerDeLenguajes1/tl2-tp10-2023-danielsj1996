using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITableroRepository
    {
        void CrearTablero(Tablero nuevoTablero);
        void ModificarTablero(int idTablero, Tablero modificarTablero);
        void EliminarTableroYTareas(int idTablero);
        Tablero ObtenerTableroPorId(int idTablero);
        List<Tablero> ListarTodosTableros();
        List<Tablero> ListarTablerosDeUsuarioEspecifico(int idUsuario);
        List<Tablero> BuscarTablerosPorNombre(string nombre);
        void CambiarPropietarioTableros(Tablero tablero);
        List<Tablero> BuscarTablerosPorPropietario(int idPropietario);
    }
}