using Biblioteca_Virtual.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Virtual.Context
{
    public class MiContext:DbContext
    {
        public MiContext(DbContextOptions options): base(options) 
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
