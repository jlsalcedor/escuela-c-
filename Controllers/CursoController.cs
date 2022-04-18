using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Escuela.Models;
using EcuelaNeiva.Models;

namespace Escuela.Controllers
{
    public class CursoController : Controller
    {


        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
            _context = context;
        }

 

        public IActionResult Index(string id)
        {
            ViewBag.Controller = "Curso";

            if (!string.IsNullOrWhiteSpace(id))
            {
                var curso = from cur in _context.Cursos
                                 where cur.Id == id
                                 select cur;

                return curso.Any() ? View(curso.SingleOrDefault()) : View("MultiCurso", _context.Cursos);


            }
            else
            {
                
                
                return View("MultiCurso", _context.Cursos);
            }


        }


        public IActionResult MultiCurso()
        {

            ViewBag.thingDinamy = "La monja";
            ViewBag.Controller = "Curso";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiCurso", _context.Cursos);
        }



        public IActionResult Edit(string id)
        {
            var Course = from cour in _context.Cursos
                         where cour.Id == id
                         select cour;
            Curso curso = Course.SingleOrDefault();

            return View("edit", curso);
        }

        [HttpPost]
        public IActionResult Edit(Curso newData, string id)
        {
            if (ModelState.IsValid)
            {

                var CourseSearch = from cour in _context.Cursos
                                   where cour.Id == id
                                   select cour;

                var Course = CourseSearch.SingleOrDefault();

                Course.Dirección = newData.Dirección;
                Course.Nombre = newData.Nombre;
                Course.Jornada = newData.Jornada;
                _context.SaveChanges();

                return RedirectToAction("MultiCurso");
            }

            return View(newData);

        }



        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {

            ViewBag.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();
                curso.EscuelaId = escuela.Id;

                _context.Cursos.Add(curso);
                _context.SaveChanges();

                return RedirectToAction("MultiCurso");
            }
            else
            {
                return View(curso);
            }        
            
        }

        public IActionResult Delete(string id)
        {
            var CourseSearch = from cour in _context.Cursos
                               where cour.Id == id
                               select cour;

            _context.Cursos.Remove(CourseSearch.FirstOrDefault());

            _context.SaveChanges();


            return RedirectToAction("MultiCurso");
        }

    }
}