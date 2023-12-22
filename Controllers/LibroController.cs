using Biblioteca_Virtual.Context;
using Biblioteca_Virtual.Models;
using Biblioteca_Virtual.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Biblioteca_Virtual.Controllers
{
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string DirectorioDestino = "wwwroot/rutaInterna";
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
        public IActionResult CrearLibro(Libro obj, IFormFile archivoPDF)
        {
            if (ModelState.IsValid && archivoPDF != null)
            {
                string directorioDestino = "RutaInterna"; // Reemplaza con la ruta deseada

                if (!Directory.Exists(directorioDestino))
                {
                    Directory.CreateDirectory(directorioDestino);
                }

                // Validar que el archivo sea PDF
                if (Path.GetExtension(archivoPDF.FileName).ToLowerInvariant() != ".pdf")
                {
                    ModelState.AddModelError("ArchivoPDF", "El archivo debe ser de tipo PDF.");
                    return View(obj);
                }

                // Guardar el archivo en el almacenamiento interno
                var fileName = $"{Guid.NewGuid()}.pdf";
                var filePath = Path.Combine(directorioDestino, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    archivoPDF.CopyTo(stream);
                }

                // Guardar la información del libro en la base de datos
                obj.RutaArchivoPDF = fileName; // Guardamos solo el nombre del archivo en la base de datos
                _context.Libros.Add(obj);
                _context.SaveChanges();

                return RedirectToAction("Ver", new { Codigo = obj.Codigo });
            }

            return View(obj);
        }

    }
}
