using System.ComponentModel.DataAnnotations;

namespace Biblioteca_Virtual.Models
{
    public class Comentario
    {
        [Key]
        public string? Descripcion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        
    }
}
