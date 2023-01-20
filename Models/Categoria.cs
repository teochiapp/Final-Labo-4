namespace WebApplicationLabo4.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string ? Descripcion { get; set; }
        public virtual List<Clubes> Club { get; set; }
    }
}