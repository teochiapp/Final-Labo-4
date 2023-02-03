using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLabo4.Data;
using WebApplicationLabo4.Models;
using WebApplicationLabo4.ModelsView;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationLabo4.Controllers
{
    public class JugadoresController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public JugadoresController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Jugador
        public async Task<IActionResult> Index(string busquedaApellido, string? busquedaNombres, int pagina = 1)
        {
            paginador paginador = new paginador()
            {
                CantidadRegistros = _context.Jugadores.Count(),
                PaginaActual = pagina,
                RegistrosxPagina = 4
            };

            var consulta = _context.Jugadores.Select(a => a);
            if (!string.IsNullOrEmpty(busquedaApellido))
            {
                consulta = consulta.Where(e => e.Apellido.Contains(busquedaApellido));
            }
            if (!string.IsNullOrEmpty(busquedaNombres))
            {
                consulta = consulta.Where(e => e.Nombres.Contains(busquedaNombres));
            }

            paginador.CantidadRegistros = consulta.Count();

            var datosAMostrar = consulta
                .Skip((paginador.PaginaActual - 1) * paginador.RegistrosxPagina)
                .Take(paginador.RegistrosxPagina);

            foreach (var item in Request.Query)
                paginador.ValoresQueryString.Add(item.Key, item.Value);

            JugadoresViewModel modelo = new JugadoresViewModel()
            {
                Jugador = await datosAMostrar.ToListAsync(),                
                Apellido = busquedaApellido,
                Nombres = busquedaNombres,
                paginador = paginador
            };
            
            return View(modelo);
        }

        // GET: Jugador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadores == null)
            {
                return NotFound();
            }

            return View(jugadores);
        }

        // GET: Jugador/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Importar()
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoImpo = archivos[0];
                var pathDestino = Path.Combine(env.WebRootPath, "importaciones");
                if (archivoImpo.Length > 0)
                {
                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoImpo.FileName);
                    string rutaCompleta = Path.Combine(pathDestino, archivoDestino);
                    using (var filestream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        archivoImpo.CopyTo(filestream);
                    };

                    using (var file = new FileStream(rutaCompleta, FileMode.Open))
                    {
                        List<string> renglones = new List<string>();
                        List<Jugadores> JugadorArch = new List<Jugadores>();

                        StreamReader fileContent = new StreamReader(file); // System.Text.Encoding.Default
                        do
                        {
                            renglones.Add(fileContent.ReadLine());
                        }
                        while (!fileContent.EndOfStream);

                        foreach (string renglon in renglones)
                        {
                            int salida;
                            string[] datos = renglon.Split(';');

                            Jugadores jugadorImportado = new Jugadores()
                            {
                                Apellido = datos[0],
                                Nombres = datos[1],
                                Biografia = datos[2]                                
                            };
                            JugadorArch.Add(jugadorImportado);
                        }
                        if (JugadorArch.Count > 0)
                        {
                            _context.Jugadores.AddRange(JugadorArch);
                            _context.SaveChanges();
                        }

                        ViewBag.cantReng = JugadorArch.Count + " de " + renglones.Count;
                    }
                }
            }

            return View();
        }

        // POST: Jugador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Apellido,Nombres,Biografia,Foto")] Jugadores jugadores)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    if (archivoFoto.Length > 0)
                    {
                        var pathDestino = Path.Combine(env.WebRootPath, "images\\jugadores");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            jugadores.Foto = archivoDestino;
                        };
                    }
                }
                _context.Add(jugadores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jugadores);
        }

        // GET: Jugador/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores.FindAsync(id);
            if (jugadores == null)
            {
                return NotFound();
            }
            return View(jugadores);
        }

        // POST: Jugador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Apellido,Nombres,Biografia,Foto")] Jugadores jugadores)
        {
            if (id != jugadores.Id)
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
                        var pathDestino = Path.Combine(env.WebRootPath, "images\\jugadores");
                        if (archivoFoto.Length > 0)
                        {
                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(jugadores.Foto))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, jugadores.Foto);
                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                jugadores.Foto = archivoDestino;
                            };
                        }
                    }
                    _context.Update(jugadores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadoresExists(jugadores.Id))
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
            return View(jugadores);
        }

        // GET: Jugador/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jugadores == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadores == null)
            {
                return NotFound();
            }

            return View(jugadores);
        }

        // POST: Jugador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugadores = await _context.Jugadores.FindAsync(id);

            var pathDestino = Path.Combine(env.WebRootPath, "images\\jugadores");

            if (!string.IsNullOrEmpty(jugadores.Foto))
            {
                string fotoAnterior = Path.Combine(pathDestino, jugadores.Foto);
                if (System.IO.File.Exists(fotoAnterior)) System.IO.File.Delete(fotoAnterior);
            }

            _context.Jugadores.Remove(jugadores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadoresExists(int id)
        {
            return _context.Jugadores.Any(e => e.Id == id);
        }
    }
}
