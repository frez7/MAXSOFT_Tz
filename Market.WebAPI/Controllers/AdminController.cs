using Market.BLL.MarketBL.Services;
using Market.DAL.Entities.Identity;
using Market.Domain.Common;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("create-user")]
        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            return await _adminService.CreateUser(request);
        }
        [HttpPost("create-shop")]
        public async Task<Response> CreateShop(CreateShopRequest request)
        {
            return await _adminService.CreateShop(request);
        }
        [HttpGet("get-shops")]
        public async Task<ListShopResponse> GetAllShops()
        {
            return await _adminService.GetAllShops();
        }
        [HttpGet("roles")]
        public async Task<List<Role>> GetAllRoles()
        {
            return await _adminService.GetAllRoles();
        }
    }
}
