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
    public class SchoolController : Controller
    {

        private EscuelaContext _context;
        public SchoolController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.thingDinamy = "La monja";
            var school =_context.Escuelas.FirstOrDefault();
            return View(school);
        }

    }
}
