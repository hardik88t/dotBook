using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotBook.Data;
using dotBook.Models;


namespace dotBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleBooksController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public SaleBooksController(DotDBcontext context)
        {
            _context = context;
        }

        // GET: api/SaleBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleBook>>> GetSaleBooks()
        {
            return await _context.SaleBooks.ToListAsync();
        }

        // GET: api/SaleBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleBook>> GetSaleBook(int id)
        {
            var saleBook = await _context.SaleBooks.FindAsync(id);

            if (saleBook == null)
            {
                return NotFound();
            }

            return saleBook;
        }

        private bool SaleBookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}
