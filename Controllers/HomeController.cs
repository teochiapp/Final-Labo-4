using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationLabo4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplicationLabo4.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationLabo4.ModelsView;

namespace WebApplicationLabo4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            var datos = _context.Clubes.Include(a => a.Categoria).Select(a => a);
            ClubesViewModel modelo = new ClubesViewModel()
            {
                Club = datos.OrderBy(modelo => modelo.Inauguracion).ToList(),                
            };

            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion");
            return View(modelo);
            //return View();
        }

        // GET: Home/Details/5
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

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}