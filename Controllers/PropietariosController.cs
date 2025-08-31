using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using vertacnik_inmobiliaria2025.Models;
using Microsoft.Extensions.Configuration;

namespace vertacnik_inmobiliaria2025.Controllers;

public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;
    private readonly RepoPropietario _repo;
    private readonly IConfiguration _config;

    public PropietariosController(RepoPropietario repo, IConfiguration config, ILogger<PropietariosController> logger)
    {
        _config = config;
        _repo = new RepoPropietario(config);
        _logger = logger;
    }

    public IActionResult Index()
    {
        var lista = _repo.ObtenerTodos();
        return View(lista);
    }

    [HttpPost]
    public IActionResult Guardar(Propietario propietario)
    {
        if (ModelState.IsValid)
        {
            if (propietario.IdPropietario > 0)
            {
                _repo.Modificacion(propietario);
            }
            else
            {
                _repo.Alta(propietario);
            }
            // Redirige a Index para recargar la lista
            return RedirectToAction("Index");
        }
        // Si hay error de validación, vuelve a mostrar el formulario en el modal
        return PartialView("_FormularioPropietario", propietario);
    }

    public IActionResult Crear()
    {
        return PartialView("_FormularioPropietario", new Propietario());
    }

    public IActionResult Editar(int id)
    {   
        var propietario = _repo.ObtenerPorId(id);
        return PartialView("_FormularioPropietario", propietario);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}