using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationLabo4.Models;

namespace WebApplicationLabo4.ModelsView
{
    public class JugadoresViewModel
    {
        public List<Jugadores> Jugador { get; set; }       
        public string? Apellido { get; set; }
        public string? Nombres { get; set; }
        public paginador paginador { get; set; }
    }

    
}
