using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebshopApi.Models;

namespace WebshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly WebshopContext _context;

        public CartsController(WebshopContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carts>>> GetCarts()
        {
            var carts = await _context.Carts.ToListAsync();

            return carts;

        }
        //api/carts/firstcart
        [HttpGet ("firstcart")]
        public async Task<ActionResult<Carts>> GetFirstCart()
        {
            var carts = await _context.Carts.FirstAsync();

            return carts;
        }
        // GET: api/Carts/5
        //csúnya/össze-vissza van írva, javítani kell
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCart>> GetCart(long id)
        {
            var cartRecord = await _context.Carts.FindAsync(id).ConfigureAwait(false);

            if (cartRecord == null)
            {
                return NotFound();
            }

            var query =
                from p1 in _context.Products
                join m1 in _context.Manufacturers
                on p1.ManufacturerID equals m1.ID
                select new Product(p1.ID, m1.Name,p1.Name,p1.Price);

            var products = await query.ToListAsync().ConfigureAwait(false);

            var orderitemsquery = from oi in _context.OrderItems
                             where oi.CartID == cartRecord.ID
                             select oi;
            var orderitems = await orderitemsquery.ToListAsync().ConfigureAwait(false);

            var cartitems = products.Join(orderitems, p => p.ID, oi => oi.ProductID, (p, v) => new CartItem(p, v.Pieces)).ToList();

            var filteredproducts = (from p in products
                       join oi in orderitems
                       on p.ID equals oi.ProductID
                       select new Product(p.ID, p.Manufacturer, p.ProductName, p.Price)).ToList();


            UserCart usercart = new UserCart();
            usercart.cartpieces = cartitems;
            usercart.products = filteredproducts;
            usercart.numberofitems = 0;
            foreach (var c in usercart.cartpieces)
            {
                usercart.numberofitems += c.pieces;   
            }

            return usercart;
        }
        public class PostCartItemArgs
        {
            public long id { get; set; }
            public long productid { get; set; }
            public long pieces { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> PostCartItem(PostCartItemArgs data)
        {
            long id = data.id;
            long productid = data.productid;
            long pieces = data.pieces;

            var cart = await _context.Carts.FindAsync(id).ConfigureAwait(false);

            if (cart == null)
            {
                return NotFound();
            }

            var orderitemquery = from oi in _context.OrderItems
                            where (oi.CartID == id && oi.ProductID == productid)
                            select oi;

            var orderitem = await orderitemquery.FirstOrDefaultAsync().ConfigureAwait(false);

            if (orderitem == null)
            {
                _context.OrderItems.Add(new OrderItems { CartID = id, Pieces = pieces, ProductID = productid });
                _context.SaveChanges();
            }
            else
            {
                orderitem.Pieces+=pieces;
                if(orderitem.Pieces == 0)
                {
                    _context.OrderItems.Remove(orderitem);
                }
                _context.SaveChanges();
            }

            return NoContent();
        }
    }
}
