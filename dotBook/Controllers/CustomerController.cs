using System.Collections.Generic;
using System.Linq;
using dotBook.Data;
using dotBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public CustomerController(DotDBcontext context)
        {
            _context = context;
        }

        // GET api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _context.Customers.ToList();
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST api/customer
        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}