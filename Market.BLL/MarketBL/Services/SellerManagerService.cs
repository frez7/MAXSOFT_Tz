using Market.BLL.Helpers;
using Market.DAL.Data.Repositories;
using Market.DAL.Entities.Identity;
using Market.Domain.Common;
using Market.Domain.Responses;

namespace Market.BLL.MarketBL.Services
{
    public class SellerManagerService
    {
        private readonly IRepository<User> _userRepository;
        private readonly GetService _getService;
        public SellerManagerService(IRepository<User> userRepository, GetService getService)
        {
            _userRepository = userRepository;
            _getService = getService;
        }
        /// <summary>
        /// Добавляет продавца в магазин к которому привязан менеджер.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> AddSellerToShop(int sellerId)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new Response(404, false, "На данный момент вы не работаете в магазине!");
            }
            var seller = await _userRepository.GetByIdAsync(sellerId);
            var sellerRoles = await _getService.GetUserRoles(sellerId);
            if (!sellerRoles.Contains("Seller"))
            {
                return new Response(400, false, "Аккаунт с таким айди не является продавцом!");
            }
            if (seller.ShopId == user.ShopId)
            {
                return new Response(400, false, "Продавец уже работает в данном магазине!");
            }
            if (seller.ShopId != null) 
            {
                return new Response(400, false, "Продавец уже работает в другом магазине!");
            }
            seller.ShopId = user.ShopId;
            await _userRepository.UpdateAsync(seller);
            return new Response(200, true, "Вы успешно добавили продавца в ваш магазин!");

        }
        /// <summary>
        /// Удаляет продавца из магазина к которому привязан менеджер.
        /// </summary>
        /// <returns></returns>
        public async Task<Response> RemoveSellerFromShop(int sellerId)
        {
            var user = await _getService.GetCurrentUser();
            if (user.ShopId == null)
            {
                return new Response(404, false, "На данный момент вы не работаете в магазине!");
            }
            var seller = await _userRepository.GetByIdAsync(sellerId);
            var sellerRoles = await _getService.GetUserRoles(sellerId);
            if (!sellerRoles.Contains("Seller"))
            {
                return new Response(400, false, "Аккаунт с таким айди не является продавцом!");
            }
            if (seller.ShopId != user.ShopId)
            {
                return new Response(400, false, "Продавец работает в другом магазине, вы не можете его удалить!");
            }
            else if (seller.ShopId == user.ShopId)
            {
                seller.ShopId = null;
                await _userRepository.UpdateAsync(seller);
                return new Response(200, true, "Вы успешно удалили продавца с вашего магазина!");
            }
            return new Response(400, true, "Произошла некая ошибка при удалении продавца!");
        }
    }
}
