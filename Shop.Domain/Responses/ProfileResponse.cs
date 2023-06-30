using Market.Domain.Common;

namespace Market.Domain.Responses
{
    public class ProfileResponse : Response
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? ShopName { get; set; }
        public List<string> Roles { get; set; }
        public ProfileResponse(int statusCode, bool success, string message, string userName, string fullName, string? shopName, List<string> roles) : base(statusCode, success, message)
        {
            UserName = userName;
            FullName = fullName;
            ShopName = shopName;
            Roles = roles;
        }
    }
}
