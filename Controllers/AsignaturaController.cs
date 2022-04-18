using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcuelaNeiva.Models;
using Escuela.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Escuela.Controllers
{
    public class AsignaturaController : Controller
    {


        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }

        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")]

        public IActionResult Index(string asignaturaId)
        {

            ViewBag.Controller = "Asignatura";

            if (!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == asignaturaId
                                 select asig;

                return asignatura.Any() ? View(asignatura.SingleOrDefault()) : View("MultiAsignatura", _context.Asignaturas);
                

            }
            else
            {
                return View("MultiAsignatura", _context.Asignaturas);
            }

            
        }


        public IActionResult MultiAsignatura()
        {
            ViewBag.Controller = "Asignatura";
            ViewBag.thingDinamy = "La monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAsignatura", _context.Asignaturas);
        }


        public IActionResult Edit(string id)
        {
            var asignaturas = from asig in _context.Asignaturas
                          where asig.Id == id
                          select asig;
            Asignatura asignatura = asignaturas.SingleOrDefault();

            return View("edit", asignatura);
        }

        [HttpPost]
        public IActionResult Edit(Asignatura newData, string id)
        {
            if (ModelState.IsValid)
            {

                var asignaturaSearch = from asig in _context.Asignaturas
                                   where asig.Id == id
                                   select asig;

                var asignaturaS = asignaturaSearch.SingleOrDefault();

                asignaturaS.Nombre = newData.Nombre;

                _context.SaveChanges();

                return RedirectToAction("MultiAsignatura");
            }

            return View(newData);

        }

        public IActionResult Create()
        {

            ViewBag.Fecha = DateTime.Now;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Asignatura asignatura)
        {

            ViewBag.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Asignaturas.Add(asignatura);
                _context.SaveChanges();

                return RedirectToAction("MultiAsignatura");
            }
            else
            {
                return View(asignatura);
            }

        }

        public IActionResult Delete(string id)
        {
            var asignaturaSearch = from asig in _context.Asignaturas
                               where asig.Id == id
                               select asig;

            _context.Asignaturas.Remove(asignaturaSearch.FirstOrDefault());

            _context.SaveChanges();


            return RedirectToAction("MultiAsignatura");
        }
    }
}
