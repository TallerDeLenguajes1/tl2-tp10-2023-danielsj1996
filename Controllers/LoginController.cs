using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.ViewModels;


namespace tl2_tp10_2023_danielsj1996.Controllers;


public class LoginController : Controller
{
    private readonly string cadenaConexion = "Data Source=DataBase/kanban.db;Cache=Shared";

    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger)
    {

        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            LoginViewModel login = new LoginViewModel();
            return View(login);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();

        }
    }

    public IActionResult Login(Login login)
    {


        try
        {
            bool validacion = false;
            Login usuarioPorIngresar = new Login();
            var query = "SELECT * FROM Usuarioa WHERE contrasenia=@contraseña AND nombre_de_usuario=@usuario";
            SQLiteParameter parameterUser = new SQLiteParameter("@usuario", login.Nombre);
            SQLiteParameter parameterPass = new SQLiteParameter("@usuario", login.Contrasenia);


            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.Add(parameterUser);
                command.Parameters.Add(parameterPass);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        validacion = true;
                        usuarioPorIngresar.Contrasenia = Convert.ToString(reader["contrasenia"]);
                        usuarioPorIngresar.Nombre = Convert.ToString(reader["nombre_de_usuario"]);
                        usuarioPorIngresar.Nivel = (NivelDeAcceso)Enum.Parse(typeof(NivelDeAcceso), Convert.ToString(reader["nivel_de_acceso"]), true);
                    }
                }
                connection.Close();
            }
            if (validacion == false)
            {
                _logger.LogWarning($"Acceso invalido - Usuario o contrasenia incorrectos");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogInformation($"El Usuario {usuarioPorIngresar.Nombre} ingresó correctamente.");
                logUsuario(usuarioPorIngresar);
                var rutaARedireccionar = new { controller = "Usuario", action = "Index" };
                return RedirectToRoute(rutaARedireccionar);
            }


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    private void logUsuario(Login usuarioPorLoguear)
    {
        HttpContext.Session.SetString("Nombre", usuarioPorLoguear.Nombre);
        HttpContext.Session.SetString("Contrasenia", usuarioPorLoguear.Contrasenia);
        HttpContext.Session.SetString("NivelDeAcceso", Convert.ToString(usuarioPorLoguear.Nivel));
    }














}