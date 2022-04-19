using System;
using System.ComponentModel.DataAnnotations;

namespace Escuela.Models
{
    public abstract class ObjetoEscuelaBase
    {
        public string Id { get; set; }


        [Display(Prompt = "Ingrese un nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(8, ErrorMessage = "El nombre debe ser mayor de 8 letras.")]
        public virtual string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre},{Id}";
        }
    }
}