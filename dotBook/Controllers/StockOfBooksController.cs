//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using dotBook.Data;
//using dotBook.Models;


//namespace dotBook.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StockOfBooksController : ControllerBase
//    {
//        private readonly DotDBcontext _context;

//        public StockOfBooksController(DotDBcontext context)
//        {
//            _context = context;
//        }

//        // GET: api/StockOfBooks
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<StockOfBook>>> GetStockOfBooks()
//        {
//            return await _context.StockOfBooks.ToListAsync();
//        }

//        // GET: api/StockOfBooks/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<StockOfBook>> GetStockOfBooks(int id)
//        {
//            var stockOfBooks = await _context.StockOfBooks.FindAsync(id);

//            if (stockOfBooks == null)
//            {
//                return NotFound();
//            }

//            return stockOfBooks;
//        }

//        // PUT: api/StockOfBooks/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutStockOfBooks(int id, StockOfBook stockOfBooks)
//        {
//            if (id != stockOfBooks.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(stockOfBooks).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!StockOfBooksExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }

//            }
//            return NoContent();
//        }

//        // POST: api/StockOfBooks
//        [HttpPost]
//        public async Task<ActionResult<StockOfBook>> PostStockOfBooks(StockOfBook stockOfBooks)
//        {
//            _context.StockOfBooks.Add(stockOfBooks);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetStockOfBooks), new { id = stockOfBooks.Id }, stockOfBooks);
//        }


//        // DELETE: api/StockOfBooks/5
//        [HttpDelete("{id}")]
//            public async Task<IActionResult> DeleteStockOfBooks(int id)
//            {
//                var stockOfBooks = await _context.StockOfBooks.FindAsync(id);
//                if (stockOfBooks == null)
//                {
//                    return NotFound();
//                }

//                _context.StockOfBooks.Remove(stockOfBooks);
//                await _context.SaveChangesAsync();

//                return NoContent();
//            }

//            private bool StockOfBooksExists(int id)
//            {
//                return _context.StockOfBooks.Any(e => e.Id == id);
//            }
//        }
//    }