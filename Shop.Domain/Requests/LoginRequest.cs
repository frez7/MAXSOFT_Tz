namespace Market.Domain.Requests
{
    /// <summary>
    /// Модель запроса входа в систему
    /// </summary>
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
