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
    //Контроллер для работы с функционалом менеджера
    public class ManagerController : Controller
    {
        private readonly ProductService _productService;
        private readonly SellerManagerService _sellerManagerService;
        public ManagerController(ProductService productService, SellerManagerService sellerManagerService)
        {
            _productService = productService;
            _sellerManagerService = sellerManagerService;
        }
        [HttpPost("shop/add/seller")]
        //Эндпоинт для добавления продавца в магазин
        public async Task<Response> AddSellerToShop(int sellerId)
        {
            return await _sellerManagerService.AddSellerToShop(sellerId);
        }
        [HttpPost("shop/remove/seller")]
        //Эндпоинт для удаления продавца из магазина
        public async Task<Response> RemoveSellerFromShop(int sellerId)
        {
            return await _sellerManagerService.RemoveSellerFromShop(sellerId);
        }

        [HttpPost("shop/create/product")]
        //Эндпоинт для создания товара в магазине
        public async Task<Response> CreateProduct(CreateProductRequest request)
        {
            return await _productService.CreateProduct(request);
        }

        [HttpGet("shop/products")]
        //Эндпоинт для отображения всех товаров в магазине
        public async Task<ListProductResponse> GetAllShopProducts()
        {
            return await _productService.GetAllShopProducts();
        }
        [HttpPut("shop/update/product")]
        //Эндпоинт для обновления товара магазина
        public async Task<Response> UpdateProduct(UpdateProductRequest request)
        {
            return await _productService.UpdateProduct(request);
        }
        [HttpDelete("shop/delete/product/{productId}")]
        //Эндпоинт для удаления товара из магазина
        public async Task<Response> DeleteProduct(int productId)
        {
            return await _productService.DeleteProduct(productId);
        }
    }
}
