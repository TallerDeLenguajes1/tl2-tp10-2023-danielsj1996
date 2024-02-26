using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.ViewModels;
using tl2_tp10_2023_danielsj1996.Repositorios;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(ILogger<HomeController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Accediendo al método Index del controlador Login.");
                return View(new LoginViewModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al acceder al método Index del controlador Login.");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Por favor, complete todos los campos.";
                _logger.LogWarning("ModelState no válido en el método Index del controlador Login.");
                return View("Index", loginViewModel);
            }
            try
            {
                var usuarioLogin = _usuarioRepository.ObtenerIDDelUsuarioLogueado(loginViewModel.Nombre!, loginViewModel.Contrasenia!);
                if (usuarioLogin == null)
                {
                    TempData["Mensaje"] = "Credenciales inválidas. Intente nuevamente.";
                    _logger.LogWarning("Intento de acceso inválido - Usuario: " + loginViewModel.Nombre + " - Clave ingresada: " + loginViewModel.Contrasenia);
                    return View("Index", loginViewModel);
                }
                else
                {
                    LogearUsuario(usuarioLogin);
                    _logger.LogInformation("El usuario " + loginViewModel.Nombre + " ingreso correctamente!");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ocurrió un error al procesar la solicitud. Por favor, inténtalo nuevamente más tarde.";
                _logger.LogError(ex, "Error al procesar la solicitud en el método Index del controlador Login.");
                return View("Index", loginViewModel);
            }
        }

        private void LogearUsuario(Usuario user)
        {
            try
            {
                HttpContext.Session.SetInt32("IdUsuario", user.IdUsuario);
                HttpContext.Session.SetString("Usuario", user.NombreDeUsuario!);
                HttpContext.Session.SetString("Rol", user.Nivel.ToString());
                _logger.LogInformation($"El usuario {user.NombreDeUsuario} se ha registrado en la sesión con ID: {user.IdUsuario} y rol: {user.Nivel}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar sesión del usuario en la sesión");
                throw;
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData["Mensaje"] = "¡La sesión se cerró exitosamente! ¡Vuelve pronto!";
                _logger.LogInformation("La sesión se cerró exitosamente para el usuario.");
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ocurrió un error al cerrar sesión. Por favor, inténtalo nuevamente más tarde.";
                _logger.LogError(ex, "Error al cerrar sesión del usuario");
                return RedirectToAction("Index", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
