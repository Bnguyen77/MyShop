using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket: BaseEntity
    {
        //Lazy Loading (virtual)
        //whenever Basket is loaded, entity Framework will also load the Basket Item.

        public virtual ICollection<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();           
        }
    }
}
