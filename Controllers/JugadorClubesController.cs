using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLabo4.Data;
using WebApplicationLabo4.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationLabo4.Controllers
{
    public class JugadorClubesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public JugadorClubesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: JugadorClubes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JugadorClub.Include(j => j.Club).Include(j => j.Jugador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JugadorClubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JugadorClub == null)
            {
                return NotFound();
            }

            var jugadorClub = await _context.JugadorClub
                .Include(j => j.Club)
                .Include(j => j.Jugador)
                .FirstOrDefaultAsync(m => m.idClub == id);
            if (jugadorClub == null)
            {
                return NotFound();
            }

            return View(jugadorClub);
        }

        // GET: JugadorClubes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["idClub"] = new SelectList(_context.Clubes, "Id", "Nombre");
            ViewData["idJugador"] = new SelectList(_context.Jugadores, "Id", "Apellido");
            return View();
        }

        // POST: JugadorClubes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,idJugador,idClub")] JugadorClub jugadorClub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jugadorClub);                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idClub"] = new SelectList(_context.Clubes, "Id", "Id", jugadorClub.idClub);
            ViewData["idJugador"] = new SelectList(_context.Jugadores, "Id", "Id", jugadorClub.idJugador);
            return View(jugadorClub);
        }

        // GET: JugadorClubes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, int? id2)
        {
            if (id == null || _context.JugadorClub == null)
            {
                return NotFound();
            }

            var jugadorClub = await _context.JugadorClub
                .Include(j => j.Club)
                .Include(j => j.Jugador)
                .FirstOrDefaultAsync(m => m.idClub == id);
            if (jugadorClub == null)
            {
                return NotFound();
            }
            ViewData["idClub"] = new SelectList(_context.Clubes, "idClub", "Id", jugadorClub.idClub);
            ViewData["idJugador"] = new SelectList(_context.Jugadores, "IdJugador", "Id", jugadorClub.idJugador);
            return View(jugadorClub);
        }

        // POST: JugadorClubes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("idJugador,idClub")] JugadorClub jugadorClub)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugadorClub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorClubExists(jugadorClub.idClub))
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
            ViewData["idClub"] = new SelectList(_context.Clubes, "Id", "Id", jugadorClub.idClub);
            ViewData["idJugador"] = new SelectList(_context.Jugadores, "Id", "Id", jugadorClub.idJugador);
            return View(jugadorClub);
        }

        // GET: JugadorClubes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id, int? id2)
        {
            if (id == null || _context.JugadorClub == null)
            {
                return NotFound();
            }

            var jugadorClub = await _context.JugadorClub
                .Include(j => j.Club)
                .Include(j => j.Jugador)
                .FirstOrDefaultAsync(m => m.idClub == id);
            if (jugadorClub == null)
            {
                return NotFound();
            }

            return View(jugadorClub);
        }

        // POST: JugadorClubes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id, int id2)
        {
            
            if (_context.JugadorClub == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JugadorClub'  is null.");
            }
            var jugadorClub = await _context.JugadorClub
                .Include(j => j.Club)
                .Include(j => j.Jugador)
                .FirstOrDefaultAsync(m => m.idClub == id);
            if (jugadorClub != null)
            {
                _context.JugadorClub.Remove(jugadorClub);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorClubExists(int id)
        {
          return _context.JugadorClub.Any(e => e.idClub == id);
        }
    }
}
