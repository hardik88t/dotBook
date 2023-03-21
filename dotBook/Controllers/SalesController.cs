using dotBook.Data;
using dotBook.Models;
using dotBook.Models.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dotBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public SalesController(DotDBcontext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales
                .Include(s => s.SaleBooks)
                .ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleBooks)
                    //.ThenInclude(sb => sb.Book)
                    //.ThenInclude(sb => sb.Sale)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(NewSale newsale)
        {
            var sale = new Sale()
            {
                CustomerId = newsale.CustomerId,
                SaleBooks = newsale.SaleBooks,
                TotalPrice = newsale.TotalPrice
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        //// PUT: api/Sales/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSale(int id, Sale sale)
        //{
        //    if (id != sale.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sale).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SaleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //// DELETE: api/Sales/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSale(int id)
        //{
        //    var sale = await _context.Sales.FindAsync(id);
        //    if (sale == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Sales.Remove(sale);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
