using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.Models
{
    public class UserCart
    {
        public List<Product> products { get; set; }
        public List <CartItem> cartpieces { get; set; }
        public long numberofitems { get; set; }
    }
}
