using Microsoft.AspNetCore.Mvc;
namespace Biblioteca_Virtual.Controllers
{
    public class CompartirController : Controller
    {
        [HttpGet]
        public IActionResult Index(string url)
        {
            ViewBag.CompartirUrl = url;
            return View("~/Views/Libro/Ver.cshtml");
        }
    }
    }
