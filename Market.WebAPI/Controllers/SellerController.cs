using Market.BLL.MarketBL.Services;
using Market.Domain.Common;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebAPI.Controllers
{
    [ApiController]
    [Route("api/seller")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
    //Контроллер для реализации работы продавца
    public class SellerController : Controller
    {
        private readonly ProductService _productService;
        public SellerController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("shop/products")]
        //Эндпоинт для возвращения всех товаров магазина
        public async Task<ListProductResponse> GetAllShopProducts()
        {
            return await _productService.GetAllShopProducts();
        }
        [HttpPut("shop/product/sell")]
        //Эндпоинт для продажи товаров из магазина
        public async Task<Response> SellProduct(int productId, int count)
        {
            return await _productService.SellProduct(productId, count);
        }
    }
}
