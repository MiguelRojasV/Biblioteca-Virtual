using Biblioteca_Virtual.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Virtual.Models
{
    public class Usuario
    {
        [Key]
        public int  Id { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Nombre_Completo { get; set; }
        [Required]
        public RolEnum Rol { get; set; }

        /*
         * agregado por el inge, causa error
        public virtual List<Comentario>? Comentarios { get; set; }
        public virtual List<Libro>? Libros { get; set; }
        */

    }
}
