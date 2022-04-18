using System;
using System.Collections.Generic;
using System.Linq;
using Escuela.Models;
using Microsoft.EntityFrameworkCore;

namespace EcuelaNeiva.Models
{
    public class EscuelaContext: DbContext
    {

       

        public EscuelaContext(DbContextOptions<EscuelaContext> options): base(options)
        {
        }

        public DbSet<EscuelaNeiva> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var school = new EscuelaNeiva();
            school.AñoDeCreación = 2005;
            school.Id = Guid.NewGuid().ToString();
            school.Nombre = "Platzi school";
            school.Ciudad = "Neiva";
            school.Pais = "Colombia";
            school.TipoEscuela = TiposEscuela.Secundaria;
            school.Dirección = "Calle 63 22a 56";

            // cargar cursos de la escuela

            var cursos = CargarCursos(school);

            // x cada curo cargar asignaturas

            var asignaturas = CargarAsignaturas(cursos);

            // x cada curo cargar alumnos

            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<EscuelaNeiva>().HasData(school);

            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());

            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());

            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());




        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {

            var listaCompleta = new List<Asignatura>();

            foreach (var curso in cursos)
            {
                var tmpList = new List<Asignatura> {
                            new Asignatura{
                                Id = Guid.NewGuid().ToString(),
                                CursoId = curso.Id,
                                Nombre="Matemáticas"
                            } ,
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Educación Física"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Castellano"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Ciencias Naturales"},
                            new Asignatura{Id = Guid.NewGuid().ToString(), CursoId = curso.Id, Nombre="Programación"}

                };

                listaCompleta.AddRange(tmpList);

            }

            return listaCompleta;
        }

        private static List<Curso> CargarCursos(EscuelaNeiva school)
        {
            return new List<Curso>(){
                        new Curso{
                            Id = Guid.NewGuid().ToString(),
                            EscuelaId = school.Id,
                            Nombre = "101",
                            Jornada = TiposJornada.Mañana,
                            Dirección = "Avenida siembre viva"
                        },
                        new Curso{ Id = Guid.NewGuid().ToString(), EscuelaId = school.Id, Nombre = "201", Jornada = TiposJornada.Mañana, Dirección = "Avenida siembre viva"},
                        new Curso{ Id = Guid.NewGuid().ToString(), EscuelaId = school.Id, Nombre = "301", Jornada = TiposJornada.Mañana, Dirección = "Avenida siembre viva"},
                        new Curso{ Id = Guid.NewGuid().ToString(), EscuelaId = school.Id, Nombre = "401", Jornada = TiposJornada.Tarde, Dirección = "Avenida siembre viva"},
                        new Curso{ Id = Guid.NewGuid().ToString(), EscuelaId = school.Id, Nombre = "501", Jornada = TiposJornada.Tarde, Dirección = "Avenida siembre viva"},
            };
        }

        private System.Collections.Generic.List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }



        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmplist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmplist);
            }
            return listaAlumnos;
        }

    }
}
