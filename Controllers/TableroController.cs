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
        private readonly ITareaRepository repoTar;
        private readonly string cadenaConexion = "Data Source = DB/kanban.db;Cache=Shared";
        private readonly ILogger<HomeController> _logger;
        public TableroController(ILogger<HomeController> logger, ITableroRepository TabRepo, ITareaRepository TarRepo)
        {
            _logger = logger;
            repo = TabRepo;
            repoTar = TarRepo;

        }

        public IActionResult Index(int idUsuario)
        {
            try
            {
                int idObtenido = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tablero> tableros = null;
                if (isAdmin())
                {
                    tableros = repo.ListarTodosTableros();
                    List<ListarTableroEspecificoViewModel> listarTablerosVM = ListarTableroEspecificoViewModel.FromTablero(tableros);
                    return View(listarTablerosVM);
                }
                else if (isOperario())
                {
                    return RedirectToAction("MostrarTablerosDeUsuarioEspecificos", new { idUsuario = idObtenido });
                }
                else
                {
                    return NotFound();
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
        public IActionResult MostrarTablerosDeUsuarioEspecificos(int idUsuario)
        {
            try
            {
                int idObtenido = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tablero> tableros = null;
                if (isOperario())
                {
                    if (idUsuario == idObtenido)
                    {
                        tableros = repo.ListarTablerosDeUsuarioEspecifico(idObtenido);
                        List<ListarTableroEspecificoViewModel> listarTablerosVM2 = ListarTableroEspecificoViewModel.FromTablero(tableros);
                        return View(listarTablerosVM2);
                    }
                    else
                    {
                        return NotFound("No tienes permisos para acceder a los tableros de otro usuario.");
                    }
                }
                else
                {
                    return NotFound();
                }


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

                Tablero nuevaTablero = Tablero.FromCrearTableroViewModel(nuevaTableroVM);
                repo.CrearTablero(nuevaTablero);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }


        public IActionResult EditarTablero(int idTablero)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                Tablero editarTablero = repo.ObtenerTableroPorId(idTablero);
                EditarTableroViewModel editarTareaVM = null;
                editarTareaVM = EditarTableroViewModel.FromTablero(editarTablero);
                if (isAdmin() || isOperario())
                {
                    return View(editarTareaVM);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult EditarTableroFromForm([FromForm] EditarTableroViewModel editarTableroVM)
        {
            try
            {
                int userId = ObtenerIDDelUsuarioLogueado(cadenaConexion);
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");
                Tablero editarTablero = Tablero.FromEditarTableroViewModel(editarTableroVM);
                repo.ModificarTablero(editarTablero);
                return RedirectToAction("Index", new { idUsuario = userId });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult EliminarTablero(int idTablero)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tablero tableroAEliminar = repo.ObtenerTableroPorId(idTablero);
                int? idUsuarioTablero = tableroAEliminar.IdUsuarioPropietario;
                if (isAdmin() || isOperario())
                {
                    return View(tableroAEliminar);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult ConfirmarEliminacionTablero(Tablero tableroAEliminar)
        {
            try
            {
                repo.EliminarTableroPorId(tableroAEliminar.IdTablero);
                return RedirectToAction("Index");


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
        private bool isOperario()
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "operario")
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
            if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "operario")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private int ObtenerIDDelUsuarioLogueado(string cadenaConexion)
        {

            string query = "SELECT * FROM Usuario WHERE nombre_de_usuario=@nombre AND contrasenia=@contrasenia";
            Console.WriteLine("Consulta SQL: " + query);
            Usuario usuarioElegido = new Usuario();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", HttpContext.Session.GetString("Nombre")));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", HttpContext.Session.GetString("Contrasenia")));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarioElegido.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        Console.WriteLine($"IdUsuario obtenido: {usuarioElegido.IdUsuario}");
                    }
                }
                connection.Close();
            }
            return usuarioElegido.IdUsuario;


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}