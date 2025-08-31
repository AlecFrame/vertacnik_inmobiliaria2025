using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using vertacnik_inmobiliaria2025.Models;

namespace vertacnik_inmobiliaria2025.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;

    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new List<Propietario>());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}