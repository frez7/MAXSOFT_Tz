using Market.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.DAL.Entities.Market
{
    /// <summary>
    /// Класс товара наследуемого от BaseEntity
    /// </summary>
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
    }
}
