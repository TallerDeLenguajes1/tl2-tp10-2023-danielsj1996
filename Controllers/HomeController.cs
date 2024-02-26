﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_danielsj1996.Models;
using tl2_tp10_2023_danielsj1996.Repositorios;

namespace tl2_tp10_2023_danielsj1996.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                if (!Autorizacion.EstaAutentificado(HttpContext))
                {
                    _logger.LogInformation("Intento de acceso sin autenticación al método Index del controlador Home. Redirigiendo al login.");
                    return RedirectToAction("Index", "Login");
                }
                _logger.LogInformation("Accediendo al método Index del controlador Home.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al acceder al método Index del controlador Home.");
                return BadRequest();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}