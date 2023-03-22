using System.Collections.Generic;
using System.Linq;
using dotBook.Data;
using dotBook.Models;
using dotBook.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public CustomersController(DotDBcontext context)
        {
            _context = context;
        }

        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(NewCustomer newcustomer)
        {
            if(newcustomer.Contact == "string")
            {
                newcustomer.Contact = "9558405050";
            }
            var customer = new Customer()
            {
                Name = newcustomer.Name,
                Address = newcustomer.Address,
                Contact= newcustomer.Contact,
                JoinDate = DateTime.Now
            };

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, NewCustomer newcustomer)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = newcustomer.Name;
            customer.Address = newcustomer.Address;
            customer.Contact = newcustomer.Contact;

            _context.Entry(customer).State = EntityState.Modified;
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

        // DELETE api/customer/5
        //[HttpDelete("{id}")]
        //public async IActionResult Delete(int id)
        //{
        //    var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChanges();

        //    return NoContent();
        //}
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}