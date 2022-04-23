using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcuelaNeiva.Models;
using Escuela.Models;

namespace Escuela.Controllers
{
    public class EscuelaNeivaController : Controller
    {
        private readonly EscuelaContext _context;

        public EscuelaNeivaController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: EscuelaNeiva
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escuelas.ToListAsync());
        }

        // GET: EscuelaNeiva/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaNeiva = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escuelaNeiva == null)
            {
                return NotFound();
            }

            return View(escuelaNeiva);
        }

        // GET: EscuelaNeiva/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EscuelaNeiva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AñoDeCreación,Pais,Ciudad,Dirección,TipoEscuela,Id,Nombre")] EscuelaNeiva escuelaNeiva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escuelaNeiva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escuelaNeiva);
        }

        // GET: EscuelaNeiva/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaNeiva = await _context.Escuelas.FindAsync(id);
            if (escuelaNeiva == null)
            {
                return NotFound();
            }
            return View(escuelaNeiva);
        }

        // POST: EscuelaNeiva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AñoDeCreación,Pais,Ciudad,Dirección,TipoEscuela,Id,Nombre")] EscuelaNeiva escuelaNeiva)
        {
            if (id != escuelaNeiva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escuelaNeiva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscuelaNeivaExists(escuelaNeiva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(escuelaNeiva);
        }

        // GET: EscuelaNeiva/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaNeiva = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escuelaNeiva == null)
            {
                return NotFound();
            }

            return View(escuelaNeiva);
        }

        // POST: EscuelaNeiva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var escuelaNeiva = await _context.Escuelas.FindAsync(id);
            _context.Escuelas.Remove(escuelaNeiva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscuelaNeivaExists(string id)
        {
            return _context.Escuelas.Any(e => e.Id == id);
        }
    }
}
