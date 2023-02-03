using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace WebApplicationLabo4.Models
{
    public class JugadorClub
    {
        [Key]
        public int Id { get; set; }
        public int idJugador { get; set; }
        [Display(Name = "Jugador")]
        public Jugadores ? Jugador { get; set; }

        public int idClub { get; set; }
        [Display(Name = "Club")]
        public Clubes? Club { get; set; }
    }
}
