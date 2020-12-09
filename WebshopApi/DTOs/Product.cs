using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.DTOs
{
    public class Product
    {
        public Product(long id, string manufacturer, string name, long price)
        {
            ProductName = name;
            Manufacturer = manufacturer;
            Price = price;
            ID = id;
        }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public long Price { get; set; }
        public long ID { get; set; }
    }
}
