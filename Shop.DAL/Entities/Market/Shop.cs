using Market.DAL.Entities.Identity;
using Shop.DAL.Common;

namespace Market.DAL.Entities.Market
{
    public class Shop : BaseEntity<int>
    {
        public string Name { get; set; }
        public User Manager { get; set; }
        public int ManagerId { get; set; }
        public List<User> Sellers { get; set; }
        public List<Product> Products { get; set; }
    }
}
