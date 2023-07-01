using Market.Domain.Common;
using Market.Domain.DTOs;

namespace Market.Domain.Responses
{
    /// <summary>
    /// Класс Response для возвращения списка товаров
    /// </summary>
    public class ListProductResponse : Response
    {
        public List<ProductDTO> Products { get; set; }
        public ListProductResponse(int statusCode, bool success, string message, List<ProductDTO> products) : base(statusCode, success, message)
        {
            Products = products;
        }
    }
}
