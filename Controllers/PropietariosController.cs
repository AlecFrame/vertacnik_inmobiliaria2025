using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using vertacnik_inmobiliaria2025.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
    [ValidateAntiForgeryToken]
    public IActionResult Guardar(Propietario propietario)
    {
        if (ModelState.IsValid)
        {
            // Si la clave no está vacía, hashearla
            if (!string.IsNullOrEmpty(propietario.Clave))
            {
                propietario.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: propietario.Clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
            }

            if (propietario.IdPropietario > 0)
            {
                _repo.Modificacion(propietario);
            }
            else
            {
                _repo.Alta(propietario);
            }
            // Si es AJAX, devolver JSON, si no, redirigir
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("Index");
            }
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

    public IActionResult Inactivar(int id)
    {
        _repo.Inactivar(id);
        return RedirectToAction("Index");
    }

    public IActionResult Activar(int id)
    {
        _repo.Activar(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}