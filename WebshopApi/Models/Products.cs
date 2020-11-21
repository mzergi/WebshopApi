using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.Models
{
    public class Products
    {
        public string Name { get; set; }
        public long ManufacturerID { get; set; }
        public long Price { get; set; }
        public long ID { get; set; }
    }
}
