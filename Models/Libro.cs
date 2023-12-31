﻿using Biblioteca_Virtual.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_Virtual.Models
{
    public class Libro
    {
        [Key]
        public int Codigo { get; set; }
        [Required]
        public string? Autor { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public string? Editorial { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Foto { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [NotMapped]
        [Display(Name = "Cargar Foto")]
        public IFormFile? FotoFile { get; set; }
        public string? Archivo { get; set; }
        [NotMapped]
        [Display(Name = "Archivo PDF")]
        [Required(ErrorMessage = "Por favor, seleccione un archivo.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".pdf" }, ErrorMessage = "El archivo debe ser un PDF.")]
        public IFormFile? ArchivoPDF { get; set; }
        public string? RutaArchivoPDF { get; internal set; }

        /*
         * agregado por el inge causa, error
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }
        */
    }
}
