using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplicationLabo4.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(40)]
        [MaxLength(40, ErrorMessage = "La descripción debe ser de 40 carácteres o inferior.")]
        [DisplayName("Descripcion")]
        public string ? Descripcion { get; set; }
        public List<Clubes>? Clubes { get; set; }
    }
}