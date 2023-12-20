using Biblioteca_Virtual.Models;

namespace Biblioteca_Virtual.Models.ViewModels
{
    public class LibroDetalleViewModel
    {
        public Libro libro { get; set; }
        public IEnumerable<Comentario> comentarios { get; set; }
    }
}
