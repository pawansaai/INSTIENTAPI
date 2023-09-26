using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INSTIENTAPI.Models;

namespace INSTIENTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly INSTIENTDbContext _context;

        public CustomersController(INSTIENTDbContext context)
        {
            _context = context;
        }

        // GET: api/GetCustomers
        [HttpGet(Name = "GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            //Fetching all the customers from the customers table in database, selecting only the name column.
            var customers = await _context.customers.Select(x => x.Name).ToListAsync();
            return Ok(customers);
        }

        // POST: api/GetOrdersByCustomer
        [HttpPost(Name = "GetOrdersByCustomer")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            //fetching all the orders data using customer ID by implementing WHERE condition.
            var orders = await _context.orders.Where(x => x.Customer_ID == customerId).ToListAsync();

            //fetching all the customers data using customer ID and selects name column.
            //To show customer name also.
            var customers = await _context.customers.Where(x => x.Id == customerId).Select(x => x.Name).ToListAsync();

            return Ok(new { Customers = customers, Orders = orders});
        }



    }
}
