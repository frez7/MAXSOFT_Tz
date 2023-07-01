using Market.Domain.Common;
using Market.Domain.DTOs;

namespace Market.Domain.Responses
{
    public class ListProfileResponse : Response
    {
        public List<ProfileDTO> Profiles { get; set; }
        public ListProfileResponse(int statusCode, bool success, string message, List<ProfileDTO> profiles) : base(statusCode, success, message)
        {
            Profiles = profiles;
        }
    }
}
