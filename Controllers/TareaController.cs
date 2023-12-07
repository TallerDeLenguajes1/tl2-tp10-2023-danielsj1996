using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaRepository repo;
        private readonly ILogger<HomeController> _logger;

        public TareaController(ILogger<HomeController> logger, ITareaRepository tareaRepo)
        {
            _logger = logger;
            repo = tareaRepo;
        }

        public IActionResult Index(int? idTablero)
        {
            try
            {

                List<Tarea> tareas = null;
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (isAdmin())
                {
                    tareas = repo.ListarTareas();
                }
                else if (idTablero.HasValue)
                {
                    tareas = repo.ListarTareasDeTablero(idTablero);
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


        [HttpGet]
        public IActionResult CrearTarea()
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
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
                Tarea nuevaTarea = Tarea.FromCrearTareaViewModel(nuevaTareaVM);
                repo.CrearTarea(nuevaTarea);
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
                Tarea tareaAEditar = repo.ObtenerTareaPorId(idTarea);
                EditarTareaViewModel tareaAModificarVM = EditarTareaViewModel.FromTarea(tareaAEditar);

                return View(tareaAModificarVM);
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
                repo.ModificarTarea(tareaAModificar);
                return RedirectToAction("Index");

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
                Tarea tareaAEliminar = repo.ObtenerTareaPorId(idTarea);
                return View(tareaAEliminar);
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
                repo.EliminarTarea(tareaAEliminar.IdTarea);
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
                Tarea tareaSeleccionada = repo.ObtenerTareaPorId(idTarea);
                AsignarTareaViewModel tareaSeleccionadaVM = AsignarTareaViewModel.FromTarea(tareaSeleccionada);
                return View(tareaSeleccionadaVM);
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
                Tarea TareaSeleccionada=Tarea.FromAsignarTareaViewModel(tareaASeleccionadaVM);
                repo.EliminarTarea(tareaASeleccionadaVM.Id);
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    }
}
