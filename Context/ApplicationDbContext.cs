using Biblioteca_Virtual.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_Virtual.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }

        public DbSet<Comentario> Comentarios { get; set; }
    }
}
