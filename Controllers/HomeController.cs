using Biblioteca_Virtual.Models;
using Biblioteca_Virtual.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biblioteca_Virtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Libro> objLibroLista = _context.Libros; 
            return View(objLibroLista);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AcercaDe()
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






