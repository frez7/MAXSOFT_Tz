using Market.DAL.Common;
using Market.DAL.Entities.Identity;

namespace Market.DAL.Entities.Market
{
    /// <summary>
    /// Класс магазина наследуемого от BaseEntity
    /// </summary>
    public class Shop : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual User Manager { get; set; }
        public int? ManagerId { get; set; }
        public virtual List<User> Sellers { get; set; }
        public List<Product> Products { get; set; }
    }
}
