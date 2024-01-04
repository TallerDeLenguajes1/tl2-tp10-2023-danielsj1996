using System.Data.SQLite;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class TareaController : Controller
    {
        private readonly string cadenadeconexion = "Data Source = DataBase/kanban.db;Cache=Shared";
        private readonly ITareaRepository repoTar;
        private readonly ITableroRepository repoTab;

        private readonly ILogger<HomeController> _logger;

        public TareaController(ILogger<HomeController> logger, ITareaRepository tareaRepo, ITableroRepository TabRepo)
        {
            _logger = logger;
            repoTar = tareaRepo;
            repoTab = TabRepo;
        }

        public IActionResult Index()
        {
            try
            {

                // if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tarea> tareas = null;
                tareas = repoTar.ListarTareas();
                // if (isAdmin())
                // {
                // }
                // //else if (idTablero.HasValue)
                // {
                //     Tablero tableroAct = repoTab.ObtenerTableroPorId(idTablero);
                //     int? ID = ObtenerIDDelUsuarioLogueado(cadenadeconexion);

                //     tareas = repoTar.ListarTareasDeTablero(idTablero);
                // }
                // //else
                // {
                //     return NotFound();
                // }
                List<ListarTareaViewModel> listarTareasVM = ListarTareaViewModel.FromTarea(tareas);
                return View(listarTareasVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult CrearTarea()
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (!isAdmin()) return NotFound();

                CrearTareaViewModel nuevaTareaVM = new CrearTareaViewModel();
                return View(nuevaTareaVM);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult CrearTareaFromForm([FromForm] CrearTareaViewModel nuevaTareaVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (!isAdmin()) return NotFound();

                Tarea nuevaTarea = Tarea.FromCrearTareaViewModel(nuevaTareaVM);
                repoTar.CrearTarea(nuevaTarea);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EditarTarea(int? idTarea)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea tareaAModificar = repoTar.ObtenerTareaPorId(idTarea);
                EditarTareaViewModel tareaAModificarVM = null;
                int? ID = ObtenerIDDelUsuarioLogueado(cadenadeconexion);

                tareaAModificarVM = EditarTareaViewModel.FromTarea(tareaAModificar);
                if (isAdmin())
                {
                    return View(tareaAModificar);
                }
                else if (idTarea.HasValue)
                {
                    if (ID == tareaAModificar.IdUsuarioPropietario)
                    {
                        return View(tareaAModificarVM);
                    }
                    else if (ID == tareaAModificar.IdUsuarioAsignado)
                    {
                        return View("EditarTareaSimple", tareaAModificarVM);
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
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult EditarTareaFromForm([FromForm] EditarTareaViewModel tareaAModificarVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea tareaAModificar = Tarea.FromEditarTareaViewModel(tareaAModificarVM);
                int? ID = tareaAModificar.IdTablero;
                repoTar.ModificarTarea(tareaAModificar);
                return RedirectToAction("Index", new { idTablero = ID });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EliminarTarea(int? idTarea)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea tareaAEliminar = repoTar.ObtenerTareaPorId(idTarea);
                int? idUsuarioTarea = tareaAEliminar.IdUsuarioPropietario;

                if (isAdmin())
                {
                    return View(tareaAEliminar);
                }
                else if (idTarea.HasValue)
                {
                    int? ID = ObtenerIDDelUsuarioLogueado(cadenadeconexion);

                    if (ID == idUsuarioTarea)
                    {
                        return View(tareaAEliminar);
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

            }

            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult EliminarTareaFromForm([FromForm] Tarea tareaAEliminar)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                repoTar.EliminarTarea(tareaAEliminar.IdTarea);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult AsignarTareaAUsuario(int? idTarea)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea tareaSeleccionada = repoTar.ObtenerTareaPorId(idTarea);
                AsignarTareaViewModel tareaSeleccionadaVM = AsignarTareaViewModel.FromTarea(tareaSeleccionada);
                int? idUsuarioProp = tareaSeleccionada.IdUsuarioPropietario;
                if (isAdmin())
                {
                    return View(tareaSeleccionada);

                }
                else if (idTarea.HasValue)
                {
                    int? ID = ObtenerIDDelUsuarioLogueado(cadenadeconexion);
                    if (ID == idUsuarioProp)
                    {
                        return View(tareaSeleccionada);
                    }
                    else { return NotFound(); }
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
        public IActionResult AsignarTareaAUsuarioFromForm([FromForm] AsignarTareaViewModel tareaASeleccionadaVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea TareaSeleccionada = Tarea.FromAsignarTareaViewModel(tareaASeleccionadaVM);
                repoTar.AsignarUsuarioATarea(TareaSeleccionada);
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
            string query = "SELECT * FROM USuario WHERE nombre_de_usuario=@nombre AND contrasenia=@contrasenia";
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
