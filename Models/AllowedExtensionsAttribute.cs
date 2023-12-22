
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Biblioteca_Virtual.Models
{
    public class AllowedExtensionsAttribute : ValidationAttribute, IValidatableObject
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (file.Length == 0)
                {
                    return ValidationResult.Success;
                }

                return !_extensions.Contains(extension.ToLower())
                    ? new ValidationResult(GetErrorMessage())
                    : ValidationResult.Success;
            }

            return new ValidationResult("Tipo de datos no admitido.");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                yield return new ValidationResult("ValidationContext no puede ser nulo.");
            }

            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var value = property?.GetValue(validationContext.ObjectInstance, null) as IFormFile;

            if (value == null)
            {
                yield return new ValidationResult("El valor proporcionado no es un archivo.");
            }

            var extension = Path.GetExtension(value?.FileName);

            if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension.ToLower()))
            {
                yield return new ValidationResult(GetErrorMessage());
            }
        }

        public string GetErrorMessage()
        {
            return $"Solo se permiten archivos con las siguientes extensiones: {string.Join(", ", _extensions)}";
        }
    }
}

