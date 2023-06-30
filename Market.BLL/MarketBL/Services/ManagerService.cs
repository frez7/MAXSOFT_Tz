using AutoMapper;
using Market.BLL.Helpers;
using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Market;
using Market.Domain.DTOs;
using Market.Domain.Responses;

namespace Market.BLL.MarketBL.Services
{
    public class ManagerService
    {
        private readonly IRepository<Shop> _shopRepository;
        private readonly ProductRepository _productRepository;
        private readonly GetService _getService;
        private readonly IMapper _mapper;
        public ManagerService(IRepository<Shop> shopRepository, GetService getService, ProductRepository productRepository,
            IMapper mapper)
        {
            _shopRepository = shopRepository;
            _getService = getService;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ListProductResponse> GetAllShopProducts()
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new ListProductResponse(404, false, "На данный момент вы не работаете в магазине!", null);
            }
            var products = await _productRepository.GetAllShopProducts((int)user.ShopId);
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);
            return new ListProductResponse(200, true, null, productsDTO);
        }
    }
}
