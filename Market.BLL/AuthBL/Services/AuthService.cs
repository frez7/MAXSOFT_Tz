using Market.BLL.Helpers;
using Market.DAL.Entities.Identity;
using Market.Domain.DTOs;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Web.Http;

namespace Market.BLL.AuthBL.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly GetService _getService;
        public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, TokenService tokenService,
            IConfiguration configuration, GetService getService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _getService = getService;
        }
        public async Task<AuthResponse> Authenticate([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new Exception("Аккаунта с таким именем не существует!");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Неверный пароль!");
            }

            var identityRoles = new List<Role>();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                identityRoles.Add(role);
            }
            var accessToken = _tokenService.CreateToken(user, identityRoles);
            await _userManager.UpdateAsync(user);

            return new AuthResponse(200, true, "Вы успешно вошли в аккаунт!"
                , accessToken);
        }
        public async Task<ProfileResponse> GetProfile()
        {
            var user = await _getService.GetCurrentUser();
            var roles = await _getService.GetUserRoles(user.Id);
            var shop = await _getService.GetUserShop(user.Id);
            var profileDTO = new ProfileDTO { Id = user.Id, FullName = user.FullName
                , Roles = roles, UserName = user.UserName, ShopName = null };
            if (shop == null)
            {
                return new ProfileResponse(200, true, null, profileDTO);
            }
            profileDTO.ShopName = shop.Name;
            return new ProfileResponse(200, true, null, profileDTO);
        }
    }
}
