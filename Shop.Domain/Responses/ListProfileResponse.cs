using Market.Domain.Common;
using Market.Domain.DTOs;

namespace Market.Domain.Responses
{
    /// <summary>
    /// Класс Response для возвращения списка профилей пользователей
    /// </summary>
    public class ListProfileResponse : Response
    {
        public List<ProfileDTO> Profiles { get; set; }
        public ListProfileResponse(int statusCode, bool success, string message, List<ProfileDTO> profiles) : base(statusCode, success, message)
        {
            Profiles = profiles;
        }
    }
}
