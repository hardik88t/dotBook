﻿using dotBook.Data;
using dotBook.EditModels;
using dotBook.Models;
using dotBook.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public BooksController(DotDBcontext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {

            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // Update Stock
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, EditBook editbook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            //book.Description = editbook.Description;
            //book.Price = editbook.Price;
            book.Stock = editbook.Stock;


            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(NewBook newbook)
        {
            if(newbook.Price == 0)
            {
                newbook.Price = 8424;
            }
            if(newbook.Stock == 0)
            {
                newbook.Stock = 512;
            }
            if(newbook.ISBN == "string")
            {
                newbook.ISBN = "462698056840";

            }
            var book = new Book()
            {
                Title = newbook.Title,
                Author = newbook.Author,
                Price = newbook.Price,
                Stock = newbook.Stock,
                Description = newbook.Description,
                Publisher = newbook.Publisher,
                ISBN= newbook.ISBN,
                ArrivalDate = DateTime.Now
        };


            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            //var newStockOfBook = new NewStockOfBook()
            //{
            //    BookId = book.Id,
            //    Quantity = newbook.Quantity,
            //};

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
