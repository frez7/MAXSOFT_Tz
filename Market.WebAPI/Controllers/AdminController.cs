﻿using Market.BLL.MarketBL.Services;
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
    //Контроллер для работы с функционалом администратора
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        private readonly SellerManagerService _sellerManagerService;
        public AdminController(AdminService adminService, SellerManagerService sellerManagerService)
        {
            _adminService = adminService;
            _sellerManagerService = sellerManagerService;
        }
        [HttpPost("create-user")]
        //Эндпоинт для создания пользователя администратором
        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            return await _adminService.CreateUser(request);
        }
        [HttpPost("create-shop")]
        //Эндпоинт для создания магазина администратором
        public async Task<Response> CreateShop(CreateShopRequest request)
        {
            return await _adminService.CreateShop(request);
        }
        [HttpGet("get-shops")]
        //Эндпоинт для возвращения всех магазинов
        public async Task<ListShopResponse> GetAllShops()
        {
            return await _adminService.GetAllShops();
        }
        [HttpGet("roles")]
        //Эндпоинт для возвращения всех ролей
        public async Task<List<Role>> GetAllRoles()
        {
            return await _adminService.GetAllRoles();
        }
        [HttpGet("all/users")]
        //Эндпоинт для показа информации о всех пользователях в базе
        public async Task<ListProfileResponse> GetAllUsers()
        {
            return await _sellerManagerService.GetAllUsers();
        }
    }
}
