using Biblioteca_Virtual.Context;
using Biblioteca_Virtual.Models;
using Biblioteca_Virtual.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace Biblioteca_Virtual.Controllers
{
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string DirectorioDestino = "wwwroot/rutaInterna";

        public string fileName { get; private set; }

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
            //Creando el QR y enviandolo a la vista
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
            var url = location.AbsoluteUri;
            QRCodeGenerator qRcodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRcodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
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
                string directorioDestino = Path.Combine(Directory.GetCurrentDirectory(), DirectorioDestino);
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

            //obj.RutaArchivoPDF = fileName;
            //_context.Libros.Add(obj);
           // _context.SaveChanges();

            // Construir la URL del libro recién creado
            //var libroUrl = Url.Action("Ver", "Libro", new { Codigo = obj.Codigo }, Request.Scheme);

            // Redirigir a la acción de compartir
          //  return RedirectToAction("Compartir", new { url = libroUrl });
        }
        //GET
        public IActionResult Delete(int? Codigo)
        {
            if (Codigo == null || Codigo == 0)
            {
                return NotFound();
            }
            var objlibro = _context.Libros.Find(Codigo);
            if (objlibro == null)
            {
                return NotFound();
            }
            return View(objlibro);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Codigo)
        {
            var obj = _context.Libros.Find(Codigo);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Libros.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index", "Usuarios", new { Id = obj.IdUsuario });
        }
        public IActionResult Descargar(int? Codigo)
        {
            if (Codigo == null || Codigo == 0)
            {
                return NotFound();
            }

            var libro = _context.Libros.Find(Codigo);

            if (libro == null)
            {
                return NotFound();
            }

            // Ruta completa del archivo en wwwroot
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), DirectorioDestino, libro.RutaArchivoPDF);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = $"{libro.Titulo}.pdf";

            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
