using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.Models
{
    public class OrderItems
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long CartID { get; set; }
        public long Pieces { get; set; }
    }
}
