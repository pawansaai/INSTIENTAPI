using Microsoft.EntityFrameworkCore;

namespace INSTIENTAPI.Models
{
    public class INSTIENTDbContext : DbContext
    {
        public INSTIENTDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Customer> customers { get; set;}
        public DbSet<Orders> orders { get; set;}
    }
}
