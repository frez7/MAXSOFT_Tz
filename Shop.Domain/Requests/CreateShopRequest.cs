namespace Market.Domain.Requests
{
    /// <summary>
    /// Модель запроса создания магазина
    /// </summary>
    public class CreateShopRequest
    {
        public string Name { get; set; }
        public int ManagerId { get; set; }
    }
}
