using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebshopApi.Models
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
