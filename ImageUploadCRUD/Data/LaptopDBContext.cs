using LaptopCRUD.Models.ImageImplementation;
using Microsoft.EntityFrameworkCore;

namespace LaptopCRUD.Data
{
    public class LaptopDBContext : DbContext
    {
        public LaptopDBContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Laptop> Laptops { get; set; }
    }
}
