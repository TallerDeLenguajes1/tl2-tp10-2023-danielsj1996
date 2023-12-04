using tl2_tp10_2023_danielsj1996.Models;

namespace tl2_tp10_2023_danielsj1996.Repositorios
{
    public interface ITableroRepository
    {
        public void CrearTablero(Tablero nuevoTablero);
        public void ModificarTablero(int idTablero, Tablero modificarTablero);
        public Tablero TreaerTableroPorId(int idTablero);
        public List<Tablero> ListarTodosTableros();
        public List<Tablero> ListarTablerosDeUsuarioEspecifico(int idUsuario);
        public void EliminarTableroPorId(int idTablero);
    }
}