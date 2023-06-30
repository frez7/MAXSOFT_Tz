using Market.Domain.Common;
using Market.Domain.DTOs;

namespace Market.Domain.Responses
{
    public class ListShopResponse : Response
    {
        public ListShopResponse(int statusCode, bool success, string message, List<ShopDTO> shop) : base(statusCode, success, message)
        {
            Shop = shop;
        }

        public List<ShopDTO> Shop { get; set; }
    }
}
