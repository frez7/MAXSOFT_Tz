using Market.BLL.MarketBL.Services;
using Market.Domain.Common;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebAPI.Controllers
{
    [ApiController]
    [Route("api/manager")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
    public class ManagerController : Controller
    {
        //Добавление DI зависимости для использования сервисов для работы с продуктом и менеджментом продавца.
        private readonly ProductService _productService;
        private readonly SellerManagerService _sellerManagerService;
        public ManagerController(ProductService productService, SellerManagerService sellerManagerService)
        {
            _productService = productService;
            _sellerManagerService = sellerManagerService;
        }
        [HttpPost("shop/add/seller")]
        public async Task<Response> AddSellerToShop(int sellerId)
        {
            return await _sellerManagerService.AddSellerToShop(sellerId);
        }
        [HttpPost("shop/remove/seller")]
        public async Task<Response> RemoveSellerFromShop(int sellerId)
        {
            return await _sellerManagerService.RemoveSellerFromShop(sellerId);
        }

        [HttpPost("shop/create/product")]
        public async Task<Response> CreateProduct(CreateProductRequest request)
        {
            return await _productService.CreateProduct(request);
        }

        [HttpGet("shop/products")]
        public async Task<ListProductResponse> GetAllShopProducts()
        {
            return await _productService.GetAllShopProducts();
        }
        [HttpPut("shop/update/product")]
        public async Task<Response> UpdateProduct(UpdateProductRequest request)
        {
            return await _productService.UpdateProduct(request);
        }
        [HttpDelete("shop/delete/product/{productId}")]
        public async Task<Response> DeleteProduct(int productId)
        {
            return await _productService.DeleteProduct(productId);
        }
    }
}
