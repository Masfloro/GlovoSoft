using System.Diagnostics;
using System.Runtime.CompilerServices;
using GlovoSoft.Integration;
using GlovoSoft.Integration.DTO;
using GlovoSoft.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlovoSoft.Controllers;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;

    private readonly ListarUsuariosApiIntegration _listarUsuarios;
    //private readonly ListarUsuarioApiIntegration _listarUsuario;
    private readonly CrearUsuarioApiIntegration _crearUsuario;

    public UsuarioController(ILogger<UsuarioController> logger, ListarUsuariosApiIntegration listarUsuarios ,CrearUsuarioApiIntegration crearUsuario)
    {
        _logger = logger;
        _listarUsuarios = listarUsuarios;
        //_listarUsuario = listarUsuario;
        _crearUsuario = crearUsuario;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarUsuario(String name, String job)
    {

        try
        {
            var response = await _crearUsuario.CreateUser(name, job);


            if (response != null)
            {
                // Mostrar mensaje de confirmación
                TempData["SuccessMessage"] = "Usuario creado correctamente.";
            }
            else
            {
                // Manejar el caso en que la creación del usuario no fue exitosa
                ModelState.AddModelError("", "Error al crear el usuario");
            }
        }

        catch (Exception ex)
        {
            _logger.LogError($"Error al crear el usuario: {ex.Message}");
            ModelState.AddModelError("", "Error al crear el usuario");
        }


        return View();
    }

    public IActionResult RegistrarUsuario()
    {

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> ListarUsuarios()
    {
        List<Usuario> users = await _listarUsuarios.ListarUsuarios();
        return View(users);
    }

    public IActionResult ListarUsuario()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
