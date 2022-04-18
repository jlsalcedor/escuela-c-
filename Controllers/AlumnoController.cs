using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Escuela.Models;
using EcuelaNeiva.Models;

namespace Escuela.Controllers
{
    public class AlumnoController : Controller
    {


        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }

 

        public IActionResult Index(string id)
        {
            ViewBag.Controller = "Alumno";

            if (!string.IsNullOrWhiteSpace(id))
            {
                var alumno = from alum in _context.Alumnos
                                 where alum.Id == id
                                 select alum;

                return alumno.Any() ? View(alumno.SingleOrDefault()) : View("MultiAlumno", _context.Alumnos);


            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);
            }


        }


        public IActionResult MultiAlumno()
        {
            ViewBag.Controller = "Alumno";
            ViewBag.thingDinamy = "La monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAlumno", _context.Alumnos);
        }


        public IActionResult Edit(string id)
        {
            var alumnos = from alum in _context.Alumnos
                         where alum.Id == id
                         select alum;
            Alumno alumno = alumnos.SingleOrDefault();

            return View("edit", alumno);
        }

        [HttpPost]
        public IActionResult Edit(Alumno newData, string id)
        {
            if (ModelState.IsValid)
            {

                var alumnoSearch = from alum in _context.Alumnos
                                   where alum.Id == id
                                   select alum;

                var alumnosS = alumnoSearch.SingleOrDefault();

                alumnosS.Nombre = newData.Nombre;
               
                _context.SaveChanges();

                return RedirectToAction("MultiAlumno");
            }

            return View(newData);

        }

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {

            ViewBag.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                //var escuela = _context.Escuelas.FirstOrDefault();
               // curso.EscuelaId = escuela.Id;

                _context.Alumnos.Add(alumno);
                _context.SaveChanges();

                return RedirectToAction("MultiAlumno");
            }
            else
            {
                return View(alumno);
            }

        }

        public IActionResult Delete(string id)
        {
            var alumnoSearch = from alum in _context.Alumnos
                               where alum.Id == id
                               select alum;

            _context.Alumnos.Remove(alumnoSearch.FirstOrDefault());

            _context.SaveChanges();


            return RedirectToAction("MultiAlumno");
        }

    }
}