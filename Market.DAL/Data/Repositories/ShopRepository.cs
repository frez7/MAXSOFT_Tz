using Market.DAL.Entities.Market;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Data.Repositories
{
    public class ShopRepository : Repository<Shop>
    {
        private readonly AppDbContext _context;
        public ShopRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> ShopHasManager(int shopId)
        {
            bool managerInShop = _context.Users.Any(u =>
                u.ShopId == shopId &&
                _context.UserRoles.Any(ur =>
                    ur.UserId == u.Id &&
                    ur.RoleId == 2
                )
            );
            return managerInShop;
        }
    }
}
