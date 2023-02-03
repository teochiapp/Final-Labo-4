using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WebApplicationLabo4.Models
{
    public class Clubes
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "El nombre debe ser de 50 carácteres o inferior.")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El resumen es requerido.")]
        [StringLength(200)]
        [MaxLength(200, ErrorMessage = "El resumen debe ser de 200 carácteres o inferior.")]
        [DisplayName("Resumen")]
        public string Resumen { get; set; }

        [Required(ErrorMessage = "La inauguración es requerida.")]
        [DisplayName("Inauguracion")]
        public DateTime Inauguracion { get; set; }
        public string ? ImagenEscudo { get; set; }

        [Required(ErrorMessage = "El pais es requerido.")]
        [DisplayName("Pais")]
        [MaxLength(30, ErrorMessage = "El resumen debe ser de 30 carácteres o inferior.")]
        public string Pais { get; set; }

        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]

        [Required(ErrorMessage = "La categoría es requerida.")]
        [DisplayName("Categoría")]
        public Categoria? Categoria { get; set; }
        public List<JugadorClub>? jugadorClub { get; set; }
    }
}
