using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLabo4.Data;
using WebApplicationLabo4.Migrations;
using WebApplicationLabo4.Models;
using WebApplicationLabo4.ModelsView;

namespace WebApplicationLabo4.Controllers
{
    public class ClubesController : Controller
    {
        private readonly ApplicationDbContext _context;        
        private readonly IWebHostEnvironment env;

        public ClubesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        
        // GET: Clubes
        public async Task<IActionResult> Index(string busquedaNombre, string? busquedaPais, int? busquedaCategoria, int pagina = 1)
        {
            paginador paginador = new paginador()
            {
                CantidadRegistros = _context.Clubes.Count(),
                PaginaActual = pagina,
                RegistrosxPagina = 5
            };
            //var appDbContext = _context.Clubes.Include(a => a.Categoria);           

            var consulta = _context.Clubes.Include(a => a.Categoria).Select(a => a);
            if (!string.IsNullOrEmpty(busquedaNombre))
            {
                consulta = consulta.Where(e => e.Nombre.Contains(busquedaNombre));                
            }
            if (!string.IsNullOrEmpty(busquedaPais))
            {
                consulta = consulta.Where(e => e.Pais.Contains(busquedaPais));                
            }
            if (busquedaCategoria.HasValue)
            {
                consulta = consulta.Where(a => a.CategoriaId == busquedaCategoria);
            }

            paginador.CantidadRegistros = consulta.Count();

            var datosAMostrar = consulta.Include(r => r.Categoria)
                .Skip((paginador.PaginaActual - 1) * paginador.RegistrosxPagina)
                .Take(paginador.RegistrosxPagina);

            foreach (var item in Request.Query)
            {
                paginador.ValoresQueryString.Add(item.Key, item.Value);
            }

            

            ClubesViewModel modelo = new ClubesViewModel()
            {
                Club = await datosAMostrar.ToListAsync(),
                Categorias = new SelectList(_context.Categoria, "Id", "Descripcion", busquedaCategoria),
                Nombre = busquedaNombre,
                Pais = busquedaPais,
                paginador = paginador,
            };

            ViewData["CategoriaId"] = paginador;
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion", busquedaCategoria);            
            return View(modelo);
        }

        // GET: Clubes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var clubes = await _context.Clubes
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubes == null)
            {
                return NotFound();
            }

            return View(clubes);
        }

        // GET: Clubes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion");
            return View();
        }

        // POST: Clubes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Resumen,Inauguracion,ImagenEscudo,Pais,CategoriaId")] Clubes clubes)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    if (archivoFoto.Length > 0)
                    {
                        var pathDestino = Path.Combine(env.WebRootPath, "images\\clubes");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            clubes.ImagenEscudo = archivoDestino;
                        };
                    }
                }
                _context.Add(clubes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                ViewData["CategoriaId"] = new SelectList(_context.Clubes, "Id", "Descripcion", clubes.CategoriaId);
            return View(clubes);
        }

        // GET: Clubes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var clubes = await _context.Clubes.FindAsync(id);
            if (clubes == null)
            {
                return NotFound();
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion", clubes.CategoriaId);

            return View(clubes);
        }

        // POST: Clubes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Resumen,Inauguracion,ImagenEscudo,Pais,CategoriaId")] Clubes clubes)
        {
            if (id != clubes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos != null && archivos.Count > 0)
                    {
                        var archivoFoto = archivos[0];
                        var pathDestino = Path.Combine(env.WebRootPath, "images\\clubes");
                        if (archivoFoto.Length > 0)
                        {
                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(clubes.ImagenEscudo))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, clubes.ImagenEscudo);
                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                clubes.ImagenEscudo = archivoDestino;
                            };
                        }
                    }
                    _context.Update(clubes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubesExists(clubes.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nombre", clubes.CategoriaId);
            return View(clubes);
        }

        // GET: Clubes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clubes == null)
            {
                return NotFound();
            }

            var clubes = await _context.Clubes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubes == null)
            {
                return NotFound();
            }

            return View(clubes);
        }

        // POST: Clubes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubes = await _context.Clubes.FindAsync(id);

            var pathDestino = Path.Combine(env.WebRootPath, "images\\clubes");

            if (!string.IsNullOrEmpty(clubes.ImagenEscudo))
            {
                string fotoAnterior = Path.Combine(pathDestino, clubes.ImagenEscudo);
                if (System.IO.File.Exists(fotoAnterior)) System.IO.File.Delete(fotoAnterior);
            }

            _context.Clubes.Remove(clubes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubesExists(int id)
        {
          return _context.Clubes.Any(e => e.Id == id);
        }
    }
}
