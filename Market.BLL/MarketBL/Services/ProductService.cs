using AutoMapper;
using Market.BLL.Helpers;
using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Identity;
using Market.DAL.Entities.Market;
using Market.Domain.Common;
using Market.Domain.DTOs;
using Market.Domain.Requests;
using Market.Domain.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Market.BLL.MarketBL.Services
{
    public class ProductService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ProductRepository _productRepository;
        private readonly GetService _getService;
        private readonly IMapper _mapper;
        public ProductService(IRepository<User> userRepository, GetService getService, ProductRepository productRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _getService = getService;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Создает продукт в магазине в котором работает менеджер.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> CreateProduct(CreateProductRequest request)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new ListProductResponse(404, false, "На данный момент вы не работаете в магазине!", null);
            }
            try
            {
                var product = new Product
                {
                    Name = request.Name.ToLower(),
                    Price = request.Price,
                    Quantity = request.Quantity,
                    ShopId = (int)user.ShopId
                };
                await _productRepository.AddAsync(product);
                return new Response(200, true, "Вы успешно добавили новый товар в свой магазин!");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    return new Response(400, false, "Товар с таким именем уже существует!");
                }
                else
                {
                    return new Response(400, false, "Произошла некая ошибка при добавлении товара!");
                }
            }
        }
        /// <summary>
        /// Обновляет продукт в магазине в котором работает менеджер.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> UpdateProduct(UpdateProductRequest request)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new ListProductResponse(404, false, "На данный момент вы не работаете в магазине!", null);
            }
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    return new Response(404, false, "Товара с таким айди не существует!");
                }
                product.Name = request.Name.ToLower();
                product.Price = request.Price;
                product.Quantity = request.Quantity;
                await _productRepository.UpdateAsync(product);
                return new Response(200, true, "Вы успешно обновили товар в магазине!");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    return new Response(400, false, "Товар с таким именем уже существует!");
                }
                else
                {
                    return new Response(400, false, "Произошла некая ошибка при обновлении товара!");
                }
            }
        }
        /// <summary>
        /// Удаляет продукт в магазине в котором работает менеджер.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> DeleteProduct(int id)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new Response(404, false, "На данный момент вы не работаете в магазине!");
            }
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Response(404, false, "Товара с таким айди не существует!");
            }
            if (user.ShopId != product.ShopId)
            {
                return new Response(400, false, "Вы не можете удалять товар, не принадлежащий вашему магазину!");
            }
            await _productRepository.DeleteAsync(id);
            return new Response(200, true, "Вы успешно удалили товар из магазина!");
        }
        /// <summary>
        /// Возвращает все продукты магазина в котором работает менеджер или продавец.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Продает указанное количество продуктов.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> SellProduct(int productId, int count)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new Response(404, false, "На данный момент вы не работаете в магазине!");
            }
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return new Response(404, false, "Товара с таким айди не существует!");
            }
            if (product.ShopId != user.ShopId)
            {
                return new Response(400, false, "Товар с таким айди принадлежит не вашему магазину!");
            }
            if (product.Quantity < count || product.Quantity == 0)
            {
                return new Response(400, false, $"Не хватает товара для продажи" +
                    $", на данный момент у вас {product.Quantity} единиц данного товара, попросите менеджера обновить количество товара!");;
            }
            product.Quantity -= count;
            await _productRepository.UpdateAsync(product);
            return new Response(200, true, $"Вы успешно продали {count} единиц указанного товара и выручили с этого: {count * product.Price} сомов."); 
        }
    }
}
