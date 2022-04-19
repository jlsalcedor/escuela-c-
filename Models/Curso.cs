using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Escuela.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage = "El nombre del curso es requerido.")]
        [StringLength(8)]
        public override string Nombre { get; set; }

        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }

        [Display(Prompt = "Direccion correspondencia", Name = "Address")]
        [Required(ErrorMessage = "La direccion del curso es requerida.")]
        [MinLength(10, ErrorMessage = "Debe escribir mas de diez letras en la direccion.")]
        public string Dirección { get; set; }

        public string EscuelaId { get; set; }

        public EscuelaNeiva EscuelaNeiva { get; set; }


    }
}