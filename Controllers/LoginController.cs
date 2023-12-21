using Biblioteca_Virtual.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Virtual.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _miContext;
        public LoginController(ApplicationDbContext miContext) 
        {
            _miContext = miContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena) 
        {
            var usuario = await _miContext.Usuarios
                                .Where(x => x.Email == correo && x.Password == contrasena)
                                .FirstOrDefaultAsync();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                TempData["LoginError"] = "Cuenta o Password Incorrecto";
                return Redirect("Index");
            }
        }
    }
}
