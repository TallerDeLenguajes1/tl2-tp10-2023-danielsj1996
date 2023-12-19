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

public IActionResult Index(){
        try
        {
            /*var rutaARedireccionar = new { controller = "Login", action = "Index" };//el tipo de var es un tipo anonimo
            return RedirectToRoute(rutaARedireccionar);*/ //tambien es valido para redireccionar
            if(!isLogin()) return RedirectToAction("Index","Login");

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

            Usuario usuarioModificar = usuarioRepository.TraerUsuarioPorId(idUsuario);
            EditarUsuarioViewModel editarUsuarioVM = null;

            if (isAdmin())
            {
                editarUsuarioVM = EditarUsuarioViewModel.FromUsuario(usuarioModificar);
            }
            else if (idUsuario.HasValue)
            {
                int? ID = ObtenerIDDelUsuarioLogueado(CadenaDeConexion);
                if (ID == idUsuario)
                {
                    editarUsuarioVM = EditarUsuarioViewModel.FromUsuario(usuarioModificar);
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

            Usuario usuarioAModificar = Usuario.FromEditarUsuarioViewModel(usuarioAModificarVM);//convertir de EditarUsuarioViewModel a Usuario
            usuarioRepository.ModificarUsuario(usuarioAModificar);
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
            return View(usuarioAEliminar);
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



