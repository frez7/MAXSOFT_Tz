using Market.DAL.Common;

namespace Market.DAL.Entities.Market
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
    }
}
