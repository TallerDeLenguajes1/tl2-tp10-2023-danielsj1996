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
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITareaRepository _tareaRepository;
        private readonly ITableroRepository _tableroRepository;
        public TareaController(ILogger<HomeController> logger, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository, ITableroRepository tableroRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _tareaRepository = tareaRepository;
            _tableroRepository = tableroRepository;
        }

        public IActionResult Index()
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogInformation("Intento de acceso sin autenticación al método Index del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                _logger.LogInformation("Accediendo al método Index del controlador Tarea.");
                return View();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al acceder al método Index del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CrearTarea()
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogInformation("Intento de acceso sin autenticación al método CrearTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (Autorizacion.EsAdmin(HttpContext) || rol == "operador")
                {
                    var viewModel = new CrearTareaViewModel()
                    {
                        ListadoUsuariosDisponibles = _usuarioRepository.TraerTodosLosUsuarios(),
                        ListadoTableros = _tableroRepository.ListarTodosTableros()
                    };
                    _logger.LogInformation("Accediendo al método CrearTarea del controlador Tarea.");
                    return View(viewModel);
                }
                _logger.LogInformation("Intento de acceso denegado al método CrearTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al acceder al método CrearTarea del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ConfirmarCrearTarea(CrearTareaViewModel tareaViewModel)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarCrearTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    if (ModelState.IsValid)
                    {
                        var nuevaTarea = new Tarea
                        {
                            NombreTarea = tareaViewModel.NombreTarea!,
                            DescripcionTarea = tareaViewModel.DescripcionTarea,
                            EstadoTarea = tareaViewModel.EstadoTarea,
                            Color = tareaViewModel.ColorTarea,
                            IdTablero = tareaViewModel.IdTablero,
                            IdUsuarioAsignado = tareaViewModel.IdUsuarioAsignado
                        };
                        _tareaRepository.CrearTarea(tareaViewModel.IdTablero, nuevaTarea);
                        _logger.LogInformation("Se ha creado una nueva tarea por el administrador.");
                        return RedirectToAction("MostrarTodasTareas");
                    }
                    _logger.LogWarning("El modelo de datos proporcionado no es válido al intentar crear una nueva tarea por el administrador. Redirigiendo al formulario de creación de tarea.");
                    return RedirectToAction("CrearTarea");
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ConfirmarCrearTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar ConfirmarCrearTarea del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult CrearTareaPorId(int idTablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogInformation("Intento de acceso sin autenticación al método CrearTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin" || rol == "operador")
                {
                    var viewModel = new CrearTareaViewModel(new Tarea())
                    {
                        ListadoUsuariosDisponibles = _usuarioRepository.TraerTodosLosUsuarios(),
                        IdTablero = idTablero
                    };
                    _logger.LogInformation("Accediendo al método CrearTareaXId del controlador Tarea.");
                    return View(viewModel);
                }
                _logger.LogInformation("Intento de acceso denegado al método CrearTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al acceder al método CrearTarea del controlador Tarea.");
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult ConfirmarCrearTareaPorId(CrearTareaViewModel tareaViewModel)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarCrearTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin" || rol == "operador")
                {
                    if (ModelState.IsValid)
                    {
                        var nuevaTarea = new Tarea
                        {
                            NombreTarea = tareaViewModel.NombreTarea!,
                            DescripcionTarea = tareaViewModel.DescripcionTarea,
                            EstadoTarea = tareaViewModel.EstadoTarea,
                            Color = tareaViewModel.ColorTarea,
                            IdTablero = tareaViewModel.IdTablero,
                            IdUsuarioAsignado = tareaViewModel.IdUsuarioAsignado
                        };
                        _tareaRepository.CrearTarea(tareaViewModel.IdTablero, nuevaTarea);
                        _logger.LogInformation($"Se ha creado una nueva tarea en una tablero especifico.");
                        return RedirectToAction("MostrarTareasTableroIdEspecifico", new { idTablero = tareaViewModel.IdTablero });
                    }
                    _logger.LogWarning($"El modelo de datos proporcionado no es válido al intentar crear una nueva tarea. Redirigiendo al formulario de creación de tarea.");
                    return RedirectToAction("CrearTareaTareaXId");
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ConfirmarCrearTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar ConfirmarCrearTareaXId del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EditarTarea(int idTarea)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ModificarTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    var tarea = _tareaRepository.ObtenerTareaPorId(idTarea);
                    if (tarea == null)
                    {
                        _logger.LogWarning($"No se encontró ninguna tarea con el ID: {idTarea} al intentar modificarla por el administrador. Redirigiendo a NotFound.");
                        return NotFound();
                    }
                    var viewModel = new EditarTareaViewModel
                    {
                        NombreTarea = tarea.NombreTarea,
                        DescripcionTarea = tarea.DescripcionTarea,
                        EstadoTarea = (EstadoTarea)(int)tarea.EstadoTarea,
                        ColorTarea = tarea.Color,
                        IdUsuarioAsignado = tarea.IdUsuarioAsignado,
                        IdTablero = tarea.IdTablero,
                        ListadoTableros = _tableroRepository.ListarTodosTableros(),
                        ListadoDeUsuarioDisponible = _usuarioRepository.TraerTodosLosUsuarios()
                    };
                    _logger.LogInformation($"Se mostró el formulario de modificación de tarea por el administrador para la tarea con ID: {idTarea}.");
                    return View(viewModel);
                }
                else if (Autorizacion.ObtenerRol(HttpContext) == "operador")
                {
                    var tarea = _tareaRepository.ObtenerTareaPorId(idTarea);
                    if (tarea == null)
                    {
                        _logger.LogWarning($"No se encontró ninguna tarea con el ID: {idTarea} al intentar modificarla por el operador. Redirigiendo a NotFound.");
                        return NotFound();
                    }
                    var viewModel = new EditarTareaViewModel
                    {
                        IdTarea = tarea.IdTarea,
                        NombreTarea = tarea.NombreTarea,
                        DescripcionTarea = tarea.DescripcionTarea,
                        EstadoTarea = (EstadoTarea)(int)tarea.EstadoTarea,
                        IdUsuarioAsignado = tarea.IdUsuarioAsignado,
                        ColorTarea = tarea.Color,
                        IdTablero = tarea.IdTablero,
                        ListadoDeUsuarioDisponible = _usuarioRepository.TraerTodosLosUsuarios()
                    };
                    _logger.LogInformation($"Se mostró el formulario de modificación de tarea por el operador para la tarea con ID: {idTarea}.");
                    return View(viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ModificarTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar ModificarTarea del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult ConfirmarEditarTarea(EditarTareaViewModel tareaViewModel)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarModificarTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (ModelState.IsValid)
                {
                    var tarea = new Tarea
                    {
                        IdTarea = tareaViewModel.IdTarea,
                        NombreTarea = tareaViewModel.NombreTarea!,
                        DescripcionTarea = tareaViewModel.DescripcionTarea,
                        EstadoTarea = tareaViewModel.EstadoTarea,
                        Color = tareaViewModel.ColorTarea,
                        IdUsuarioAsignado = tareaViewModel.IdUsuarioAsignado,
                        IdTablero = tareaViewModel.IdTablero
                    };
                    _tareaRepository.ModificarTarea(tareaViewModel.IdTarea, tarea);
                    _logger.LogInformation($"Se modificó la tarea con ID: {tareaViewModel.IdTarea} correctamente.");
                    if (Autorizacion.EsAdmin(HttpContext))
                    {
                        return RedirectToAction("MostrarTodasTareas");
                    }
                    else
                    {
                        return RedirectToAction("MostrarTareasTableroIdEspecifico", new { idTablero = tarea.IdTablero });
                    }
                }
                _logger.LogWarning("ModelState no válido al intentar confirmar la modificación de la tarea. Redirigiendo al Index de Tarea.");
                return RedirectToAction("Index", "Tarea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar ConfirmarModificarTarea del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EliminarTarea(int idTarea)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método EliminarTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var tarea = _tareaRepository.ObtenerTareaPorId(idTarea);
                if (tarea == null)
                {
                    _logger.LogWarning($"No se encontró ninguna tarea con el ID: {idTarea} al intentar eliminarla. Redirigiendo a NotFound.");
                    return NotFound();
                }
                _logger.LogInformation($"Se mostró la confirmación de eliminación para la tarea con ID: {idTarea}.");
                return View(tarea);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar EliminarTarea del controlador Tarea.");
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult ConfirmarEliminarTarea(Tarea tarea)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarEliminarTarea del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                int idTab = tarea.IdTablero;
                _logger.LogInformation($"Se eliminó la tarea con ID: {tarea.IdTarea} correctamente.");
                _tareaRepository.EliminarTarea(tarea.IdTarea);
                var rol = Autorizacion.ObtenerRol(HttpContext);
                if (rol == "admin")
                {
                    return RedirectToAction("MostrarTodasTareas");
                }
                else if (rol == "operador")
                {
                    return RedirectToAction("MostrarTareasTableroIdEspecifico", new { idTablero = idTab });
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método ConfirmarEliminarTarea del controlador Tarea. Redirigiendo a AccesoDenegado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar ConfirmarEliminarTarea del controlador Tarea.");
                return BadRequest();
            }
        }



        [HttpGet]
        public IActionResult MostrarTodasLasTareas(string nombreBusqueda)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método MostrarTodasTareas del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    List<Tarea> tareas;
                    if (string.IsNullOrEmpty(nombreBusqueda))
                    {
                        tareas = _tareaRepository.ListarTodasLasTareas();
                    }
                    else
                    {
                        tareas = _tareaRepository.BuscarTareasPorNombre(nombreBusqueda);
                    }
                    var tareaVM = tareas.Select(u => new TareaViewModel
                    {
                        IdTableroVM = u.IdTablero,
                        IdTareaVM = u.IdTarea,
                        NombreTareaVM = u.NombreTarea,
                        ColorVM = u.Color,
                        EstadoTareaVM = u.EstadoTarea,
                        DescripcionTareaVM = u.DescripcionTarea,
                        IdUsuarioAsignadoVM = u.IdUsuarioAsignado.HasValue ? u.IdUsuarioAsignado.Value : 0,
                        NombreUsuarioAsignadoVM = u.NombreUsuarioAsignado,
                        NombreDelTableroPerteneceVM = u.NombreDelTableroPertenece
                    }).ToList();
                    var viewModel = new ListarTareaViewModel(tareaVM);
                    _logger.LogInformation("Se mostraron todas las tareas correctamente.");
                    return View("MostrarTodasTareas", viewModel);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado al método MostrarTodasTareas del controlador Tarea. Redirigiendo a AccesoDenegado.");
                    return View("~/Views/AccesoDenegado.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar MostrarTodasTareas del controlador Tarea.");
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult MostrarTareasUsuarioEspecifico(int idUsuario)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método MostrarTareasUsuarioEspecifico del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var todasLasTareas = _tareaRepository.ListarTareasDeUsuario(idUsuario);
                var tareaVM = todasLasTareas.Select(u => new TareaViewModel
                {
                    IdTareaVM = u.IdTarea,
                    IdTableroVM = u.IdTablero,
                    NombreTareaVM = u.NombreTarea,
                    ColorVM = u.Color,
                    EstadoTareaVM = u.EstadoTarea,
                    DescripcionTareaVM = u.DescripcionTarea,
                    NombreDelTableroPerteneceVM = u.NombreDelTableroPertenece
                }).ToList();
                var viewModel = new ListarTareaViewModel(tareaVM);
                _logger.LogInformation($"Se mostraron todas las tareas del usuario con ID {idUsuario} correctamente.");
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar MostrarTareasUsuarioEspecifico del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult MostrarTareasTableroIdEspecifico(int idTablero)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin autenticación al método MostrarTareasTableroIdEspecifico del controlador Tarea. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                var todasLasTareas = _tareaRepository.ListarTareasDeTablero(idTablero);
                var tareaVM = todasLasTareas.Select(u => new TareaViewModel
                {
                    IdTableroVM = u.IdTablero,
                    IdTareaVM = u.IdTarea,
                    NombreTareaVM = u.NombreTarea,
                    ColorVM = u.Color,
                    EstadoTareaVM = u.EstadoTarea,
                    DescripcionTareaVM = u.DescripcionTarea,
                    IdUsuarioAsignadoVM = u.IdUsuarioAsignado.HasValue ? u.IdUsuarioAsignado.Value : 0,
                    NombreUsuarioAsignadoVM = u.NombreUsuarioAsignado,
                    NombreDelTableroPerteneceVM = u.NombreDelTableroPertenece
                }).ToList();
                var viewModel = new ListarTareaViewModel(tareaVM);
                _logger.LogInformation($"Se mostraron todas las tareas del tablero con ID {idTablero} correctamente.");
                return View("MostrarTareasTableroIdEspecifico", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar MostrarTareasTableroIdEspecifico del controlador Tarea.");
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult BuscarTareasPorNombre(string nombre)
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método BuscarTareasPorNombre del controlador de tarea.");
                    return RedirectToAction("Index", "Login");
                }
                if (Autorizacion.EsAdmin(HttpContext))
                {
                    return MostrarTodasLasTareas(nombre);
                }
                else
                {
                    _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método BuscarTareasPorNombre del controlador de tarea.");
                    return View("~/Views/AccesoDenegado.cshtml");
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
