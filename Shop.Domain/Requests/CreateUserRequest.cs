namespace Market.Domain.Requests
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
