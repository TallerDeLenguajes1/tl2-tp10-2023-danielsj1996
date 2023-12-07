using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class TableroController : Controller
    {
        private readonly ITableroRepository repo;
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
                List<Tablero> tableros = null;
                if (!isLogin()) return RedirectToAction("Index", "Login");
                if (isAdmin())
                {
                    tableros = repo.ListarTodosTableros();
                }
                else if (idUsuario.HasValue)
                {
                    tableros = repo.ListarTablerosDeUsuarioEspecifico(idUsuario);
                }
                else
                {
                    return NotFound();
                }
                List<ListarTableroViewModel> listarTablerosVM = ListarTableroViewModel.FromTarea(tableros);
                return View(listarTablerosVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult AgregarTablero()
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                CrearTableroViewModel nuevoTableroVM = new CrearTableroViewModel();
                return View(nuevoTableroVM);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult AgregarTableroFromForm([FromForm] CrearTableroViewModel nuevoTableroVM)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");
                Tablero nuevoTablero = Tablero.FromCrearTableroViewModel(nuevoTableroVM);
                repo.CrearTablero(nuevoTablero);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult EditarTablero(int? idTablero)
        {
            try
            {
                if (!isLogin()) return RedirectToAction("Index", "Login");
                Tablero tableroAModificar = repo.ObtenerTableroPorId(idTablero);
                EditarTableroViewModel tableroAModificarVM = EditarTableroViewModel.FromTablero(tableroAModificar);
                return View(tableroAModificarVM);
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
                if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
                if (!isLogin()) return RedirectToAction("Index", "Login");
                Tablero tableroAModificar = Tablero.FromEditarTableroViewModel(editarTableroVM);
                repo.ModificarTablero(tableroAModificar);
                return RedirectToAction("Index");
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
                if (!isLogin()) return RedirectToAction("Index", "Login");
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
        if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin"){
            return true;
        }else{
            return false;
        }
    }
    private bool isLogin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "simple"){
            return true;
        }else{
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