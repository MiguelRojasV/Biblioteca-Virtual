using Biblioteca_Virtual.Context;
using Biblioteca_Virtual.Models;
using Biblioteca_Virtual.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_Virtual.Controllers
{
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LibroController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Ver(int? Codigo)
        {
            if (Codigo == null || Codigo == 0)
            {
                return NotFound();
            }
            var libroDelaBase = _context.Libros.Find(Codigo);
            if (libroDelaBase == null)
            {
                return NotFound();
            }
            IEnumerable<Comentario> objComentarioLista = _context.Comentarios
            .Where(c => c.Codigo == Codigo)
            .ToList();
            var viewModel = new LibroDetalleViewModel
            {
                libro = libroDelaBase,
                comentarios = objComentarioLista.ToList()
            };
            return View(viewModel);
        }

        //GET
        public IActionResult CrearComentario(int? Codigo)
        {
            Comentario obj = new Comentario();
            obj.Codigo = Convert.ToInt32(Codigo);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearComentario(Comentario obj)
        {
            if (ModelState.IsValid)
            {
                _context.Comentarios.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Ver", new { Codigo = obj.Codigo });
            }      
            return View(obj);
        }
        //GET
        public IActionResult CrearLibro()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearLibro(Libro obj)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Ver", new { Codigo = obj.Codigo });
            }
            return View(obj);
        }


    }
}
