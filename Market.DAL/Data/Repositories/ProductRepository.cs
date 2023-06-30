using Market.DAL.Entities.Market;

namespace Market.DAL.Data.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllShopProducts(int shopId)
        {
            var products = _context.Products.Where(p => p.ShopId == shopId);
            return products.ToList();
        }
    }
}
