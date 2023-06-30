using AutoMapper;
using Market.BLL.Helpers;
using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Identity;
using Market.DAL.Entities.Market;
using Market.Domain.Common;
using Market.Domain.DTOs;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System.Web.Http;

namespace Market.BLL.MarketBL.Services
{
    public class AdminService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<IdentityUserRole<int>> _identityUserRoleRepository;
        private readonly ShopRepository _shopRepository;
        private readonly IMapper _mapper;
        private readonly GetService _getService;
        public AdminService(IRepository<Role> roleRepository, IRepository<User> userRepository, IRepository<IdentityUserRole<int>> identityUserRoleRepository,
            ShopRepository shopRepository, IMapper mapper, GetService getService)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _identityUserRoleRepository = identityUserRoleRepository;
            _shopRepository = shopRepository;
            _mapper = mapper;
            _getService = getService;
        }
        /// <summary>
        /// Метод для возвращения всех ролей, которые есть в системе.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            return (List<Role>)roles;
        }
        /// <summary>
        /// Метод для создания аккаунта администратором.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role == null)
            {
                return new Response(404, false, "Роли с таким айди не существует!");
            }
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                Email = request.UserName,
                NormalizedEmail = request.UserName.ToUpper(),
                PasswordHash = hasher.HashPassword(null, request.Password),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = request.FullName,

            };
            await _userRepository.AddAsync(user);
            var userRole = new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = request.RoleId
            };
            await _identityUserRoleRepository.AddAsync(userRole);
            return new Response(200, true, "Вы успешно создали новый профиль для сотрудника!");
        }

        public async Task<Response> CreateShop(CreateShopRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.ManagerId);
            var roles = await _getService.GetUserRoles(user.Id);
            if (!roles.Contains("Manager"))
            {
                return new Response(400, false, "Пользователь с таким айди не является менеджером!");
            }
            if (user.ShopId != null)
            {
                return new Response(400, false, "Данный пользователь уже является менеджером в другом магазине!");
            }
            var shop = new Shop { Name = request.Name, ManagerId = request.ManagerId };
            await _shopRepository.AddAsync(shop);
            user.ShopId = shop.Id;
            await _userRepository.UpdateAsync(user);
            return new Response(200, true, "Вы успешно создали магазин!");
        }
        public async Task<ListShopResponse> GetAllShops()
        {
            var shops = await _shopRepository.GetAllAsync();
            var shopsDTO = _mapper.Map<List<ShopDTO>>(shops);
            return new ListShopResponse(200, true, null, shopsDTO);
        }
    }
}
