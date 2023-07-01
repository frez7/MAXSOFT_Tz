namespace Market.Domain.DTOs
{
    /// <summary>
    /// DTO Товара
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
