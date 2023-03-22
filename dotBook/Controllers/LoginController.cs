using dotBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DotDBcontext _context;

        public LoginController(DotDBcontext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Authenticate(LoginRequest request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == 0);

            if (customer == null)
            {
                return NotFound();
            }


            // Check if the email and password are valid
            if (request.Email == customer.Contact && request.Password == customer.Name)
            {
                if (request.Email != "")
                {
                    // Generate a token or perform any necessary action to authenticate the user
                    string token = GenerateToken(email: request.Email);
                    //"address": "dotBook",
                    customer.Address = token;
                    _context.Entry(customer).State = EntityState.Modified;
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }

                    // Return a success response with the token
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest("Really Bad Request");
                }
            }
            else
            {
                // Return a failed response with an error message
                return BadRequest(new { Message = "Invalid email or password" });
            }
        }

        private string GenerateToken(string email)
        {
            // Implement your own token generation logic here
            // This is just a simple example
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }

    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
