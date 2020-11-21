using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebshopApi.Models;

namespace WebshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly WebshopContext _context;

        public ProductItemsController(WebshopContext context)
        {
            _context = context;
        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductItems()
        {
            var query =
                from p1 in _context.Products
                join m1 in _context.Manufacturers
                on p1.ManufacturerID equals m1.ID
                select new { id = p1.ID, manufacturer = m1.Name, productName = p1.Name, price = p1.Price };

            var products = await query.ToListAsync().ConfigureAwait(false);

            return products
                .Select(p => new Product(p.id, p.manufacturer, p.productName, p.price))
                .ToList();
        }

        // GET: api/ProductItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductItem(long id)
        {
            var productItem = await _context.Products.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            return productItem;
        }

        // PUT: api/ProductItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductItem(long id, Products productItem)
        {
            if (id != productItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(productItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProductItem(Products productItem)
        {
            _context.Products.Add(productItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductItemExists(productItem.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetProductItem), new { id = productItem.ID }, productItem);
        }

        // DELETE: api/ProductItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProductItem(string id)
        {
            var productItem = await _context.Products.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productItem);
            await _context.SaveChangesAsync();

            return productItem;
        }

        private bool ProductItemExists(long id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
