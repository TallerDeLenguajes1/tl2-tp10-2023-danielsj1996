using System.Data.SQLite;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;
using tl2_tp10_2023_danielsj1996.ViewModels;


namespace tl2_tp10_2023_danielsj1996.Controllers;

public class UsuarioController : Controller
{

    private readonly string CadenaDeConexion;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly ILogger<HomeController> _logger;

    public UsuarioController(ILogger<HomeController> logger, IUsuarioRepository usuarioRep, string cadenaDeConexion)
    {

        _logger = logger;
        usuarioRepository = usuarioRep;
        CadenaDeConexion = cadenaDeConexion;
    }

    public IActionResult Index()
    {
        try
        {
            if (!isLogin()) return RedirectToAction("Index", "Login");

            List<Usuario> usuarios = usuarioRepository.TraerTodosLosUsuarios();
            List<ListarUsuarioViewModel> listaUsuariosVM = ListarUsuarioViewModel.FromUsuario(usuarios);//convertir de List<Usuario> a List<listarUsuarioViewModel>
            return View(listaUsuariosVM);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult AgregarUsuario()
    {
        try
        {
            if (!isLogin()) return RedirectToAction("Index", "Login");
            CrearUsuarioViewModel nuevoUsuarioVM = new CrearUsuarioViewModel();
            return View(nuevoUsuarioVM);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.ToString());
            return BadRequest();
        }


    }
    [HttpPost]
    public IActionResult AgregarUsuarioFromForm([FromForm] CrearUsuarioViewModel newUsuarioVM)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
            if (!isLogin()) return RedirectToAction("Index", "Login");

            Usuario newUsuario = Usuario.FromCrearUsuarioViewModel(newUsuarioVM);//convertir de CrearUsuarioViewModel a Usuario
            usuarioRepository.CrearUsuario(newUsuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditarUsuario(int? idUsuario)
    {
        try
        {
            if (!isLogin()) return RedirectToAction("Index", "Login");

            Usuario editarUsuario = usuarioRepository.TraerUsuarioPorId(idUsuario);
            EditarUsuarioViewModel editarUsuarioVM = null;

            if (isAdmin())
            {
                editarUsuarioVM = EditarUsuarioViewModel.FromUsuario(editarUsuario);
            }
            else if (idUsuario.HasValue)
            {
                int? ID = ObtenerIDDelUsuarioLogueado(CadenaDeConexion);
                if (ID == idUsuario)
                {
                    editarUsuarioVM = EditarUsuarioViewModel.FromUsuario(editarUsuario);
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
            return View(editarUsuarioVM);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult EditarUsuarioFromForm([FromForm] EditarUsuarioViewModel usuarioAModificarVM)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Login");
            //RedirectToRoute (new {}).....
            if (!isLogin()) return RedirectToAction("Index", "Login");

            Usuario editarUsuario = Usuario.FromEditarUsuarioViewModel(usuarioAModificarVM);//convertir de EditarUsuarioViewModel a Usuario
            usuarioRepository.ModificarUsuario(editarUsuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult EliminarUsuario(int? idUsuario)
    {
        try
        {
            if (!isLogin()) return RedirectToAction("Index", "Login");
            Usuario usuarioAEliminar = usuarioRepository.TraerUsuarioPorId(idUsuario);

            if (isAdmin())
            {
                return View(usuarioAEliminar);
            }
            else if (idUsuario.HasValue)
            {
                int? ID = ObtenerIDDelUsuarioLogueado(CadenaDeConexion);


                if (ID == idUsuario)
                {
                    return View(usuarioAEliminar);
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
    public IActionResult EliminarFromForm(Usuario usuarioAEliminar)
    {
        try
        {
            if (!isLogin()) return RedirectToAction("Index", "Login");

            usuarioRepository.EliminarUsuarioPorId(usuarioAEliminar.IdUsuario);
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
    public IActionResult Logout()
    {
        // Realizar las tareas de deslogueo aquí
        HttpContext.Session.Clear(); // Limpiar la sesión

        // Redirigir al usuario a la página de inicio de sesión u otra página deseada
        return RedirectToAction("Index", "Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



