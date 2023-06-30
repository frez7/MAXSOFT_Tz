using Market.BLL.MarketBL.Services;
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
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }
        [HttpGet("shop/products")]
        public async Task<ListProductResponse> GetAllShopProducts()
        {
            return await _managerService.GetAllShopProducts();
        }
    }
}
