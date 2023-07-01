namespace Market.Domain.Requests
{
    /// <summary>
    /// Модель запроса обновления товара
    /// </summary>
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
