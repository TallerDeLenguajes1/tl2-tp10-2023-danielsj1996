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
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITableroRepository _tableroRepository;
        private readonly ITareaRepository _tareaRepository;
        public TableroController(ILogger<HomeController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _tableroRepository = tableroRepository;
            _tareaRepository = tareaRepository;

        }

        public IActionResult Index()
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método Index del controlador de Tableros. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin" || rol == "operador")
                {
                    _logger.LogInformation("Acceso exitoso al método Index del controlador de Tableros.");
                    return View();
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método Index del controlador de Tableros debido a un rol no autorizado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método Index del controlador de Tableros.");
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult CrearTablero()
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método CrearTablero del controlador de Tableros. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    var viewModel = new CrearTableroViewModel(new Tablero())
                    {
                        ListadoUsuarios = _usuarioRepository.TraerTodosLosUsuarios()
                    };
                    _logger.LogInformation("Acceso exitoso al método CrearTablero del controlador de Tableros.");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método CrearTablero del controlador de Tableros debido a un rol no autorizado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método CrearTablero del controlador de Tableros.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ConfirmarCrearTablero(CrearTableroViewModel tableroViewModel)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarCrearTablero. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    if (ModelState.IsValid)
                    {
                        var tablero = new Tablero
                        {
                            NombreDeTablero = tableroViewModel.NombreDeTablero,
                            DescripcionDeTablero = tableroViewModel.DescripcionDeTablero,
                            IdUsuarioPropietario = tableroViewModel.IdUsuarioPropietario
                        };
                        _tableroRepository.CrearTablero(tablero);
                        _logger.LogInformation("Se ha creado exitosamente un nuevo tablero.");
                        return RedirectToAction("MostrarTodosLosTableros");
                    }
                    _logger.LogWarning("Intento de crear un tablero con datos no válidos.");
                    return RedirectToAction("CrearTablero");
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ConfirmarCrearTablero debido a un rol no autorizado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método ConfirmarCrearTablero.");
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult EditarTablero(int idTablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación: Alguien intentó acceder sin estar logueado al método ModificarTablero.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    var tablero = _tableroRepository.ObtenerTableroPorId(idTablero);
                    if (tablero == null)
                    {
                        _logger.LogWarning($"No se encontró ningún tablero con el ID: {idTablero}");
                        return NotFound();
                    }
                    var viewModel = new EditarTableroViewModel
                    {
                        NombreDeTablero = tablero.NombreDeTablero!,
                        DescripcionDeTablero = tablero.DescripcionDeTablero,
                        IdUsuarioPropietario = tablero.IdUsuarioPropietario!,
                        ListadoUsuarios = _usuarioRepository.TraerTodosLosUsuarios()
                    };
                    _logger.LogInformation($"Se ha cargado el tablero con ID: {idTablero} para su modificación.");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ModificarTablero.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método ModificarTablero.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ConfirmarEditarTablero(EditarTableroViewModel viewModel)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación: Alguien intentó acceder sin estar logueado al método ConfirmarModificarTablero.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    if (ModelState.IsValid)
                    {
                        var tablero = new Tablero
                        {
                            NombreDeTablero = viewModel.NombreDeTablero!,
                            IdUsuarioPropietario = viewModel.IdUsuarioPropietario
                        };
                        if (string.IsNullOrEmpty(viewModel.DescripcionDeTablero))
                        {
                            tablero.DescripcionDeTablero = null;
                        }
                        else
                        {
                            tablero.DescripcionDeTablero = viewModel.DescripcionDeTablero;
                        }
                        _tableroRepository.ModificarTablero(viewModel.IdTablero, tablero);
                        _logger.LogInformation($"Se ha modificado el tablero con ID: {viewModel.IdTablero}");
                        return RedirectToAction("MostrarTodosLosTableros");
                    }
                    else
                    {
                        _logger.LogWarning("ModelState no es válido en ConfirmarModificarTablero.");
                        return View(viewModel);
                    }
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ConfirmarModificarTablero.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método ConfirmarModificarTablero.");
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult EliminarTablero(int idTablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método EliminarTablero. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    var tablero = _tableroRepository.ObtenerTableroPorId(idTablero);
                    if (tablero == null)
                    {
                        _logger.LogWarning($"No se encontró ningún tablero con el ID: {idTablero}");
                        return NotFound();
                    }
                    var tareasAsociadas = _tareaRepository.ListarTareasDeTablero(idTablero);
                    if (tareasAsociadas.Any())
                    {
                        TempData["Mensaje"] = "Eliminar este tablero también eliminará las tareas asociadas. ¿Está seguro de que desea proceder?";
                    }
                    _logger.LogInformation($"Mostrando vista para eliminar el tablero con ID: {idTablero}");
                    return View(tablero);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método EliminarTablero debido a un rol no autorizado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método EliminarTablero.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ConfirmarEliminacionTablero(Tablero tablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarEliminar. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    _tableroRepository.EliminarTableroYTareas(tablero.IdTablero);
                    _logger.LogInformation($"Tablero con ID {tablero.IdTablero} y sus tareas asociadas han sido eliminados.");
                    return RedirectToAction("MostrarTodosLosTableros");
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ConfirmarEliminar debido a un rol no autorizado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método ConfirmarEliminar.");
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult MostrarTodosLosTableros(string nombreBusqueda)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación: Alguien intentó acceder sin estar logueado al método MostrarTodosTablero.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    List<Tablero> tableros;
                    if (string.IsNullOrEmpty(nombreBusqueda))
                    {
                        tableros = _tableroRepository.ListarTodosTableros();
                    }
                    else
                    {
                        tableros = _tableroRepository.BuscarTablerosPorNombre(nombreBusqueda);
                    }
                    var tablerosVM = tableros.Select(tablero => new TableroViewModel
                    {
                        IdTableroVM = tablero.IdTablero,
                        IdUsuarioPropietarioVM = tablero.IdUsuarioPropietario,
                        NombreTableroVM = tablero.NombreDeTablero!,
                        DescripcionVM = tablero.DescripcionDeTablero,
                        NombrePropietarioVM = tablero.NombreDePropietario!
                    }).ToList();
                    var viewModel = new ListarTablerosViewModel(tablerosVM);
                    _logger.LogInformation("Mostrando todos los tableros.");
                    return View("MostrarTodosLosTableros", viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método MostrarTodosTablero.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método MostrarTodosTablero.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult MostrarTableroId(int idTablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación: Alguien intentó acceder sin estar logueado al método MostrarTodosTablero.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin" || rol == "operador")
                {
                    Tablero tablero = _tableroRepository.ObtenerTableroPorId(idTablero);
                    if (tablero == null)
                    {
                        _logger.LogWarning($"No se encontró ningún tablero con el ID: {idTablero}");
                        return NotFound();
                    }
                    List<Tarea> tareas = _tareaRepository.ListarTareasDeTablero(idTablero);
                    var viewModel = new TableroViewModel
                    {
                        IdTableroVM = tablero.IdTablero,
                        NombreTableroVM = tablero.NombreDeTablero,
                        DescripcionVM = tablero.DescripcionDeTablero,
                        ListaDeTareas = tareas.ToList()
                    };
                    _logger.LogInformation("Mostrando el tablero.");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método MostrarTodosTablero.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método MostrarTableroId.");
                return BadRequest();
            }
        }


            [HttpGet]
        public IActionResult ListarTablerosDeUsuarioEspecifico(int idUsuario)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación: Alguien intentó acceder sin estar logueado al método ListarTablerosDeUsuarioEspecifico.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin" || rol == "operador")
                {
                    var tableros = _tableroRepository.ListarTablerosDeUsuarioEspecifico(idUsuario);
                    var tablerosVM = tableros.Select(tablero => new TableroViewModel
                    {
                        IdTableroVM = tablero.IdTablero,
                        IdUsuarioPropietarioVM = tablero.IdUsuarioPropietario,
                        NombreTableroVM = tablero.NombreDeTablero!,
                        DescripcionVM = tablero.DescripcionDeTablero,
                        NombrePropietarioVM = tablero.NombreDePropietario!
                    }).ToList();
                    var viewModel = new ListarTablerosViewModel(tablerosVM);
                    _logger.LogInformation($"Mostrando tableros del usuario con ID: {idUsuario}.");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ConfirmarModificarTablero.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método ListarTablerosDeUsuarioEspecifico.");
                return BadRequest();
            }
        }

      [HttpGet]
        public IActionResult BuscarTableroPorNombre(string nombre)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método BuscarTableroPorNombre del controlador de tableros.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    return MostrarTodosLosTableros(nombre);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método BuscarTableroPorNombre del controlador de tableros.");
                    return View("AccesoDenegado", "Usuario");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar tableros por nombre");
                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}