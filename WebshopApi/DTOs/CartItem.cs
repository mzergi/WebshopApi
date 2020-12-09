using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.DTOs
{
    public class CartItem
    {
        public CartItem (Product _product, long _pieces)
        {
            product = _product;
            pieces = _pieces;
        }
        public Product product { get; set; }
        public long pieces { get; set; }
    }
}
