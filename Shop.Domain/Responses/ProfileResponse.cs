using Market.Domain.Common;
using Market.Domain.DTOs;

namespace Market.Domain.Responses
{
    public class ProfileResponse : Response
    {
        /// <summary>
        /// Класс Response для возвращения профиля
        /// </summary>
        public ProfileDTO Profile { get; set; }
        public ProfileResponse(int statusCode, bool success, string message, ProfileDTO profile) : base(statusCode, success, message)
        {
            Profile = profile;
        }
    }
}
