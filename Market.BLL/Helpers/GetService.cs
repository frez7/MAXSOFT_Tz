using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Identity;
using Market.DAL.Entities.Market;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Market.BLL.Helpers
{
    public class GetService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public GetService(IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor,
            IRepository<Shop> shopRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _shopRepository = shopRepository;
            _userManager = userManager;
        }
        public async Task<User> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out var id);
            var user = await _userRepository.GetByIdAsync(id);
            return user;

        }
        public async Task<int> GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out var id);
            return id;
        }
        public async Task<List<string>> GetUserRoles(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            return (List<string>)roles;
        }
        public async Task<Shop> GetUserShop(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user.ShopId == null)
            {
                return null;
            }
            Shop shop = await _shopRepository.GetByIdAsync((int)user.ShopId);
            return shop;
        }
    }
}
