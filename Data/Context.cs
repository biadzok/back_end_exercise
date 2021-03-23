using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}
        public DbSet<Values> Values {get; set;}
        public DbSet<Products> product_table { get; set; }
        public DbSet<Customers> customer_table { get; set; }
        public DbSet<Orders> order_table { get; set; }
        public DbSet<User> user { get; set; }
    }
}