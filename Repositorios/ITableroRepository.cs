using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITableroRepository
    {
        public void CrearTablero(Tablero nuevoTablero);
        public List<Tablero> ListarTodosTableros();
        public Tablero ObtenerTableroPorId(int? idTablero);
        public void EliminarTableroPorId(int? idTablero);
        public void ModificarTablero(Tablero tablero);
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int? idUsuario);
        public void InhabilitarDeUsuario(int? IdUsuario);
    }
}