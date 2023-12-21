using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Virtual.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        [Required]
        public int Codigo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;


        /*
         * Agregado por el Inge, causa error
         * //foreing keys
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        */

    }
}
