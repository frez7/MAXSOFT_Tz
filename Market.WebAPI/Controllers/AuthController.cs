using Market.BLL.AuthBL.Services;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            return await _authService.Authenticate(request);
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<ProfileResponse> GetProfile()
        {
            return await _authService.GetProfile();
        }
    }
}
