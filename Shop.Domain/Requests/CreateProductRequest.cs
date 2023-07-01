namespace Market.Domain.Requests
{
    /// <summary>
    /// Модель запроса создания товара
    /// </summary>
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
