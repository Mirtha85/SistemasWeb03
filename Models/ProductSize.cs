namespace SistemasWeb01.Models
{
    public class ProductSize
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TallaId { get; set; }
        public Talla Talla { get; set; }

        public int? Quantity { get; set; }
    }
}
