using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplicationLabo4.Models
{
    public class Jugadores
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "El apellido debe ser inferior a 50 carácteres.")]
        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Los nombres son requeridos.")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Los nombres deben ser inferiores a 50 carácteres.")]
        [DisplayName("Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La biografía es requerida.")]
        [StringLength(199)]
        [MaxLength(199, ErrorMessage = "La descripción debe ser inferior a 200 carácteres.")]
        [DisplayName("Biografia")]
        public string Biografia { get; set; }

        [DisplayName("Foto")]
        public string ? Foto { get; set; }
        public List<JugadorClub>? jugadorClub { get; set; }
    }
}
