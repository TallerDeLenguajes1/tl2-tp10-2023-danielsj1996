using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class TableroController : Controller
    {
        private readonly ITableroRepository repo;
        private readonly string cadenaConexion = "Data Source = DB/kamban.db;Cache=Shared";
        private readonly ILogger<HomeController> _logger;
        public TableroController(ILogger<HomeController> logger, ITableroRepository TabRepo)
        {
            _logger = logger;
            repo = TabRepo;
        }

        public IActionResult Index(int? idUsuario)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tablero> tableros = null;
                if (isAdmin())
                {
                    tableros = repo.ListarTodosTableros();
                }
                else if (idUsuario.HasValue)
                {
                    int? ID = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                    if (ID == idUsuario)
                    {
                        tableros = repo.ListarTablerosDeUsuarioEspecifico(idUsuario);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
                List<ListarTableroViewModel> listarTablerosVM = ListarTableroViewModel.FromTablero(tableros);
                return View(listarTablerosVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CrearTablero()
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (!isAdmin()) return NotFound();

                CrearTableroViewModel nuevaTableroVM = new CrearTableroViewModel();
                return View(nuevaTableroVM);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult CrearTableroFromForm([FromForm] CrearTableroViewModel nuevaTableroVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (!isAdmin()) return NotFound();

                Tablero nuevaTablero= Tablero.FromCrearTableroViewModel(nuevaTableroVM);
                repo.CrearTablero(nuevaTablero);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult ModificarTablero(int? idTablero)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tablero tableroAModificar = repo.ObtenerTableroPorId(idTablero);
                EditarTableroViewModel tableroAModificarVM = null;

                if (!isAdmin())
                {
                    tableroAModificarVM = EditarTableroViewModel.FromTablero(tableroAModificar);
                }
                else if (idTablero.HasValue)
                {
                    int? ID = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                    if (ID == tableroAModificar.IdUsuarioPropietario)
                    {

                        tableroAModificarVM = EditarTableroViewModel.FromTablero(tableroAModificar);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
                return View(tableroAModificarVM);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult ModificarTableroFromForm([FromForm] EditarTableroViewModel editarTableroVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tablero tableroAModificar = Tablero.FromEditarTableroViewModel(editarTableroVM);
                int? ID = tableroAModificar.IdUsuarioPropietario;
                repo.ModificarTablero(tableroAModificar);
                return RedirectToAction("Index", new { idUsuario = ID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        public IActionResult EliminarTablero(int? idTablero)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tablero tableroAEliminar = repo.ObtenerTableroPorId(idTablero);
                int? idUsuarioTablero = tableroAEliminar.IdUsuarioPropietario;
                if (isAdmin())
                {
                    return View(tableroAEliminar);
                }
                else if (idTablero.HasValue)
                {
                    int? ID = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                    if (ID == idUsuarioTablero)
                    {
                        return View(tableroAEliminar);

                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return View(tableroAEliminar);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        public IActionResult EliminarFromForm(Tablero tableroAEliminar)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");

                repo.EliminarTableroPorId(tableroAEliminar.IdTablero);
                return RedirectToAction("Index", "Usuario");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        private bool isAdmin()
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isLogin()
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "simple")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private int? ObtenerIDDelUsuarioLogueado(string? cadenaConexion)
        {
            int? ID = 0;
            string query = "SELECT * FROM Usuario WHERE nombre_de_usuario=@nombre AND contrasenia=@contrasenia";
            Usuario usuarioElegido = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", HttpContext.Session.GetString("nombre")));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", HttpContext.Session.GetString("contrasenia")));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ID = Convert.ToInt32(reader["id"]);
                    }
                }
                connection.Close();
            }
            return (ID);


        }

      
      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}