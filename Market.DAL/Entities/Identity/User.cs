using Market.DAL.Entities.Market;
using Microsoft.AspNetCore.Identity;

namespace Market.DAL.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public virtual Shop Shop { get; set; }
        public int? ShopId { get; set; }
    }
}
