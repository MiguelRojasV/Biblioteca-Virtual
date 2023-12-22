using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_Virtual.Context;
using Biblioteca_Virtual.Models;
using Biblioteca_Virtual.Models.ViewModels;

namespace Biblioteca_Virtual.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public IActionResult Index(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var objusuario = _context.Usuarios.Find(Id);
            if (objusuario == null)
            {
                return NotFound();
            }
            IEnumerable<Libro> objLibroLista = _context.Libros
            .Where(l => l.IdUsuario == Id)
            .ToList();
            var viewModel = new UsuarioLibroViewModel
            {
                usuario = objusuario,
                libros = objLibroLista.ToList()
            };
            return View(viewModel);
        }
        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var usuario = _context.Usuarios.Find(Id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario obj)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(obj);
                _context.SaveChanges();                
                return RedirectToAction("Index", new { Id = obj.Id });
            }
            return View("Index", new { Id = obj.Id });
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var usuario = _context.Usuarios.Find(Id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {
            var obj = _context.Usuarios.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(obj);
            _context.SaveChanges();            
            return RedirectToAction("Index","Home");
        }
        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
