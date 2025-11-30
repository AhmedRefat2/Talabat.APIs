using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = null!;
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
