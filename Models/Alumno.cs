using System;
using System.Collections.Generic;

namespace Escuela.Models
{
    public class Alumno: ObjetoEscuelaBase
    {
        public string CursoId { get; set; }

        public Curso curso { get; set; }

        public List<Evaluación> Evaluaciones { get; set; }
    }
}