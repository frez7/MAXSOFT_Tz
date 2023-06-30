using Market.DAL.Data;
using Market.DAL.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WeatherForecastController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("test")]
        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }
    }
}