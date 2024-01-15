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
        private readonly string cadenadeconexion = "Data Source = DB/kanban.db;Cache=Shared";
        private readonly ITareaRepository repoTar;
        private readonly ITableroRepository repoTab;

        private readonly ILogger<HomeController> _logger;

        public TareaController(ILogger<HomeController> logger, ITareaRepository tareaRepo, ITableroRepository TabRepo)
        {
            _logger = logger;
            repoTar = tareaRepo;
            repoTab = TabRepo;
        }

        public IActionResult Index(int idTablero)
        {
            try
            {

                if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tarea> tareas = null;
                int idUsuario = ObtenerIDDelUsuarioLogueado(cadenadeconexion);
                if (isAdmin())
                {
                    tareas = repoTar.ListarTareas();
                }
                else
                {
                    return NotFound();
                }
                List<ListarTareaViewModel> listarTareasVM = ListarTareaViewModel.FromTarea(tareas);
                return View(listarTareasVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
        public IActionResult MostrarTareaDeTablero(int idTablero)
        {
            try
            {

                if (!isLogin()) return RedirectToAction("Index", "Login");
                List<Tarea> tareas = null;
                if (isAdmin())
                {
                    tareas = repoTar.ListarTareasDeTablero(idTablero);
                }
                if (isOperario())
                {
                    tareas = repoTar.ListarTareasDeTablero(idTablero);
                }


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

                Tarea editarTarea = repoTar.ObtenerTareaPorId(idTarea);
                EditarTareaViewModel editarTareaVM = null;
                editarTareaVM = EditarTareaViewModel.FromTarea(editarTarea);
                if (isAdmin())
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
        public IActionResult EditarTareaFromForm([FromForm] EditarTareaViewModel editarTareaVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");

                Tarea editarTarea = Tarea.FromEditarTareaViewModel(editarTareaVM);
                repoTar.ModificarTarea(editarTarea);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Console.WriteLine(ex.Message);
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
        public IActionResult ConfirmarEliminacionTarea(Tarea tareaAEliminar)
        {
            try
            {
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
