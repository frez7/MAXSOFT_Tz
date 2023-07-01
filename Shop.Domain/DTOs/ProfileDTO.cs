using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.DTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? ShopName { get; set; }
        public List<string> Roles { get; set; }
    }
}
