using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITableroRepository
    {
        public Tablero CrearTablero(Tablero nuevoTablero);
        public List<Tablero> ListarTodosTableros();
        public Tablero ObtenerTableroPorId(int? idTablero);
        public void EliminarTableroPorId(int? idTablero);
        public Tablero ModificarTablero(Tablero tablero);
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int? idUsuario);
        public void InhabilitarDeUsuario(int? IdUsuario);
    }
}