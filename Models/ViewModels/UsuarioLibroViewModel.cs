using Biblioteca_Virtual.Models;

namespace Biblioteca_Virtual.Models.ViewModels
{
    public class UsuarioLibroViewModel
    {
        public Usuario usuario { get; set; }
        public IEnumerable<Libro> libros { get; set; }
    }
}
