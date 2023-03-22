using dotBook.Data;
using dotBook.Models;
using dotBook.EditModels;
using dotBook.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            Sale sale = new Sale();
            sale.SaleDate = DateTime.Now;

            int totalPrice = 0;
            
            if (!CustomerExists(newsale.CustomerId))
            {
                return BadRequest("Customer " + newsale.CustomerId + " Not Found!");
            }
            sale.SaleBooks = new List<SaleBook>(); // Initialize the SaleBooks property
            try
            {
                foreach (var (nsb, sb) in from nsb in newsale.NewSaleBook
                                          let sb = new SaleBook()
                                          select (nsb, sb))
                {
                    if (!BookExists(nsb.BookId))
                    {
                        return BadRequest("Book " + sb.BookId + " not Found!");
                    }

                    sb.BookId = nsb.BookId;
                    if (nsb.Quantity == 0)
                    {
                        sb.Quantity = 3;
                    }
                    else
                    {
                        sb.Quantity = nsb.Quantity;
                    }

                    if (nsb.Price != 0)
                    {
                        sb.Price = nsb.Price;
                    }
                    //sb.Price = await _context.Books.FindAsync(sb.BookId).Price;
                    var book = await _context.Books.FindAsync(sb.BookId);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    sb.Price = book.Price;
                    book.Stock -= sb.Quantity;
                    totalPrice = book.Price * sb.Quantity;
                    _context.Entry(book).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }

                    sale.SaleBooks.Add(sb);
                }
            }
            catch(Exception ex)
            {
                throw new NotImplementedException("Not Implemented, error : " + ex);

            }

            sale.CustomerId = newsale.CustomerId;
            sale.TotalPrice = totalPrice;


            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        private void ThrowIfNull(ICollection<NewSaleBook>? newSaleBook)
        {
            throw new NotImplementedException("Thrown as salebooks is null");
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
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}
