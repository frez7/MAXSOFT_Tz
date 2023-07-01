namespace Market.Domain.Requests
{
    /// <summary>
    /// Модель запроса создания пользователя администратором
    /// </summary>
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
