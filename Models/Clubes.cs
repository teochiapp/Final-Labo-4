using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLabo4.Models
{
    public class Clubes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Resumen { get; set; }
        public DateTime Inauguracion { get; set; }
        public string ImagenEscudo { get; set; }
        public string Pais { get; set; }

        [ForeignKey("Categoria")]
        public virtual List<Categoria> Categoria { get; set; }
    }
}
