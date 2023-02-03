using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationLabo4.Models;

namespace WebApplicationLabo4.ModelsView
{
    public class ClubesViewModel
    {
        public List<Clubes> Club { get; set; }
        public SelectList Categorias { get; set; }
        public string? Nombre { get; set; }
        public string? Pais { get; set; }
        public paginador paginador { get; set; }
    }

    public class paginador
    {
        public int PaginaActual { get; set; }
        public int CantidadRegistros { get; set; }
        public int RegistrosxPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((decimal)CantidadRegistros / RegistrosxPagina);
        public Dictionary<string, string> ValoresQueryString { get; set; } = new Dictionary<string, string>();
    }
}
