using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;

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

        public IActionResult Index()
        {
            List<Tarea> tareas = null;
            if (!isLogin()) return RedirectToAction("Index", "Login");
            if (isAdmin())
            {
                tareas = repo.ListarTareas();
            }
            return View(todasLasTareas);
        }


        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tarea nuevaTarea)
        {
            tareaRepository.CrearTarea(nuevaTarea.IdTablero, nuevaTarea);
            return RedirectToAction("Index");
        }

        public IActionResult Modificar(int id)
        {
            Tarea tarea = tareaRepository.ObtenerTareaPorId(id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult Modificar(int id, Tarea tareaModificada)
        {
            tareaRepository.ModificarTarea(id, tareaModificada);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            Tarea tarea = tareaRepository.ObtenerTareaPorId(id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult Eliminar(int id, Tarea tareaEliminada)
        {
            tareaRepository.EliminarTarea(id);
            return RedirectToAction("Index");
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
    }
}
