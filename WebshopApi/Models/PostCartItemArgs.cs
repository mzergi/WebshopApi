using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopApi.Models
{
    public class PostCartItemArgs
    {
        public long id { get; set; }
        public long productid { get; set; }
        public long pieces { get; set; }
    }
}
