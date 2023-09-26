using INSTIENTAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INSTIENTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly INSTIENTDbContext _context;

        public SummaryController(INSTIENTDbContext context)
        {
            _context = context;
        }

        // GET: api/GetSummary
        [HttpGet(Name = "GetSummary")]
        public async Task<IActionResult> GetSummary()
        {
            //Fetching all customer data and sort by country(Alphabetical order).
            var customers = await _context.customers.OrderBy(x => x.Country).ToListAsync();

            //Fetching all orders data.
            var orders = await _context.orders.ToListAsync();

            List<SummaryModel> summary = new List<SummaryModel>(); //List of Summary(To display results).
            foreach (var user in customers)
            {
                int count = 0;
                int sum = 0;

                for (int i =0;i<orders.Count();i++)
                {
                    var order = orders[i];
                    if (user.Id == order.Customer_ID)
                    {
                        count++;
                        sum += order.Amount;
                        orders.RemoveAt(i); //This is to enhance speed by reducing the order count 
                    }
                }

                //creating obj of summary model and adding it to the list.
                summary.Add(new SummaryModel
                {
                    Name = user.Name,
                    Count = count,
                    TotalAmount = sum,
                    Country = user.Country
                    
                });
            }


            return Ok(summary);
        }
    }
}
