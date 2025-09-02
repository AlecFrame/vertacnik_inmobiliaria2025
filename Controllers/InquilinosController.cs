using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using vertacnik_inmobiliaria2025.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace vertacnik_inmobiliaria2025.Controllers;

public class InquilinosController : Controller
{
    private readonly ILogger<InquilinosController> _logger;
    private readonly RepoInquilino _repo;
    private readonly IConfiguration _config;

    public InquilinosController(RepoInquilino repo, IConfiguration config, ILogger<InquilinosController> logger)
    {
        _config = config;
        _repo = new RepoInquilino(config);
        _logger = logger;
    }

    public IActionResult Index()
    {
        var lista = _repo.ObtenerTodos();
        return View(lista);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Guardar(Inquilino inquilino)
    {
        if (ModelState.IsValid)
        {
            if (inquilino.IdInquilino > 0)
            {
                _repo.Modificacion(inquilino);
            }
            else
            {
                _repo.Alta(inquilino);
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
        else
        {
            // Si es AJAX, devolver JSON con errores, si no, redirigir
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errores });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }

    public IActionResult Crear()
    {
        return PartialView("_FormularioInquilino", new Inquilino());
    }

    public IActionResult Editar(int id)
    {
        var inquilino = _repo.ObtenerPorId(id);
        if (inquilino == null)
        {
            return NotFound();
        }
        return PartialView("_FormularioInquilino", inquilino);
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