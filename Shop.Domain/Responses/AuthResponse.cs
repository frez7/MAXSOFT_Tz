using Market.Domain.Common;

namespace Market.Domain.Responses
{
    /// <summary>
    /// Класс Response для авторизации
    /// </summary>
    public class AuthResponse : Response
    {
        public string AccessToken { get; set; }
        public AuthResponse(int statusCode, bool success, string message, string accessToken) : base(statusCode, success, message)
        {
            AccessToken = accessToken;
        }
    }
}
