using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;


namespace tl2_tp10_2023_danielsj1996.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;


    public UsuarioController(ILogger<HomeController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
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
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al Index del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                _logger.LogInformation("El usuario con permisos de administrador accedió al Index del controller Usuario.");
                return View();
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al Index del controlador de usuarios.");
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
    public IActionResult CrearUsuario()
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al Index del controllador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                var viewModel = new CrearUsuarioViewModel(new Usuario());
                _logger.LogInformation("El usuario con permisos de administrador accedió a crear un usuario.");
                return View(viewModel);
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al metodo CrearUsuario del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Crear al Usuario");
            return BadRequest();
        }
    }


    [HttpPost]
    public IActionResult ConfirmarCrearUsuario(CrearUsuarioViewModel usuarioViewModel)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método ConfirmarCrearUsuario del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                if (ModelState.IsValid)
                {
                    if (_usuarioRepository.ExisteUsuario(usuarioViewModel.NombreDeUsuario!))
                    {
                        ModelState.AddModelError("NombreDeUsuario", "El nombre de usuario ya existe.");
                        return View("CrearUsuario", usuarioViewModel);
                        // return RedirectToAction("CrearUsuario");
                    }
                    var usuario = new Usuario
                    {
                        NombreDeUsuario = usuarioViewModel.NombreDeUsuario!,
                        Contrasenia = usuarioViewModel.Contrasenia!,
                        Nivel = usuarioViewModel.NivelDeAcceso,
                    };
                    _usuarioRepository.CrearUsuario(usuario);
                    _logger.LogInformation("Se ha creado un nuevo usuario por el administrador.");
                    return RedirectToAction("MostrarTodosLosUsuarios");
                }
                _logger.LogWarning("Intento de crear un usuario con datos no válidos.");
                return View(usuarioViewModel);
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ConfirmarCrearUsuario del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Confirmar la Crearcion del Usuario");
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditarUsuario(int idUsuario)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método ModificarUsuario del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                var usuario = _usuarioRepository.TraerUsuarioPorId(idUsuario);
                if (usuario == null)
                {
                    _logger.LogWarning($"No se encontró ningún usuario con el ID: {idUsuario}");
                    return NotFound();
                }
                var viewModel = new EditarUsuarioViewModel
                {
                    NombreDeUsuario = usuario!.NombreDeUsuario,
                    Contrasenia = usuario.Contrasenia,
                    NivelDeAcceso = usuario.Nivel
                };
                _logger.LogInformation($"Accediendo a la vista de modificar usuario para el usuario con ID: {idUsuario}");
                return View(viewModel);
            }
            else
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método ModificarUsuario del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Modificar el Usuario");
            return BadRequest();
        }
    }

    [HttpPost]
    [HttpPost]
    public IActionResult ConfirmarEditarUsuario(EditarUsuarioViewModel viewModel)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método ConfirmarModificarUsuario del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                if (ModelState.IsValid)
                {
                    var usuario = new Usuario
                    {
                        NombreDeUsuario = viewModel.NombreDeUsuario!,
                        Contrasenia = viewModel.Contrasenia!,
                        Nivel = viewModel.NivelDeAcceso
                    };
                    _usuarioRepository.ModificarUsuario(viewModel.IdUsuario, usuario);
                    _logger.LogInformation($"Se ha modificado el usuario con ID: {viewModel.IdUsuario}");
                    return RedirectToAction("MostrarTodosLosUsuarios");
                }
                _logger.LogWarning("ModelState no es válido en ConfirmarModificarUsuario");
                return View(viewModel);
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ConfirmarModificarUsuario del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Confirmar La Modificacion del Usuario");
            return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult EliminarUsuario(int idUsuario)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al Index del controllador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                var usuario = _usuarioRepository.TraerUsuarioPorId(idUsuario);
                if (usuario == null)
                {
                    _logger.LogWarning($"Se intentó acceder a la vista de eliminar el usuario con ID {idUsuario}, pero el usuario no existe en la base de datos.");
                    return NotFound();
                }

                var tablerosAsociados = _tableroRepository.BuscarTablerosPorPropietario(idUsuario);
                var listaDeUsuario = _usuarioRepository.TraerTodosLosUsuarios();
                if (tablerosAsociados.Any())
                {
                    TempData["IdUsuarioEliminar"] = idUsuario;
                    TempData["Mensaje"] = "todos los tablero que pertenecian al anterior usuario de cambiaran por el nuevo";
                    return View("SeleccionarNuevoPropietario", (listaDeUsuario, idUsuario));
                }
                else
                {
                    _logger.LogInformation($"Accediendo a la vista de eliminar el usuario con ID: {idUsuario}");
                    return View(usuario);
                }
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método EliminarUsuario del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Eliminar el Usuario");
            return BadRequest();
        }
    }

       [HttpPost]
    public IActionResult ConfirmarEliminar(Usuario user)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin autenticación al método ConfirmarEliminar del controlador Usuario. Redirigiendo al login.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                var tareasAsociadas = _tareaRepository.ListarTareasDeUsuario(user.IdUsuario);
                foreach (var tarea in tareasAsociadas)
                {
                    tarea.IdUsuarioAsignado = null;
                    _tareaRepository.CambiarPropietarioTarea(tarea);
                }
                _usuarioRepository.EliminarUsuarioPorId(user.IdUsuario);
                _logger.LogInformation("Se elimino al usuario.");
                return RedirectToAction("MostrarTodosLosUsuarios");
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado al método ConfirmarEliminar del controlador usuario. Redirigiendo a AccesoDenegado.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al ejecutar ConfirmarEliminar del controlador Usuario.");
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult ConfirmarEliminarUsuarioConTablerosAsigYTareas(int nuevoPropietario, int idUsuarioEliminar)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método ConfirmarEliminar del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                var tablerosAsociados = _tableroRepository.BuscarTablerosPorPropietario(idUsuarioEliminar);
                foreach (var tablero in tablerosAsociados)
                {
                    tablero.IdUsuarioPropietario = nuevoPropietario;
                    _tableroRepository.CambiarPropietarioTableros(tablero);
                }
                var tareasAsociadas = _tareaRepository.ListarTareasDeUsuario(idUsuarioEliminar);
                foreach (var tarea in tareasAsociadas)
                {
                    tarea.IdUsuarioAsignado = null;
                    _tareaRepository.CambiarPropietarioTarea(tarea);
                }
                _usuarioRepository.EliminarUsuarioPorId(idUsuarioEliminar);
                _logger.LogInformation($"Se ha cambiado el propietario de los tableros del usuario con ID {idUsuarioEliminar} al usuario con ID {nuevoPropietario}");
                return RedirectToAction("MostrarTodosUsuarios");
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método ConfirmarEliminar del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Confirmar la Eliminacion del Usuario");
            return BadRequest();
        }
    }
    public IActionResult MostrarTodosLosUsuarios(string nombreBusqueda)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método MostrarTodosUsuarios del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                List<Usuario> usuarios;

                if (string.IsNullOrEmpty(nombreBusqueda))
                {
                    usuarios = _usuarioRepository.TraerTodosLosUsuarios();
                }
                else
                {
                    usuarios = _usuarioRepository.BuscarUsuarioPorNombre(nombreBusqueda);
                }

                var usauarioVM = usuarios.Select(u => new UsuarioViewModel
                {
                    IdUsuarioVM = u.IdUsuario,
                    NombreDeUsuarioVM = u.NombreDeUsuario,
                    ContraseniaVM = u.Contrasenia,
                    NivelVM = u.Nivel
                }).ToList();
                var viewModel = new ListarUsuariosViewModel(usauarioVM);
                _logger.LogInformation("Mostrando todos los usuarios");
                return View("MostrarTodosLosUsuarios", viewModel);
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método MostrarTodosUsuarios del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al mostrar todos los usuarios");
            return BadRequest();
        }
    }

        [HttpGet]
    public IActionResult BuscarUsuarioPorNombre(string nombre)
    {
        try
        {
            if (!Autorizacion.EstaAutentificado(HttpContext))
            {
                _logger.LogWarning("Intento de acceso sin loguearse: Alguien intentó acceder sin estar logueado al método BuscarUsuarioPorNombre del controlador de usuarios.");
                return RedirectToAction("Index", "Login");
            }
            if (Autorizacion.EsAdmin(HttpContext))
            {
                return MostrarTodosLosUsuarios(nombre);
            }
            else
            {
                _logger.LogWarning("Intento de acceso denegado: Usuario sin permisos de administrador intentó acceder al método BuscarUsuarioPorNombre del controlador de usuarios.");
                return View("~/Views/AccesoDenegado.cshtml");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar usuario por nombre");
            return BadRequest();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



