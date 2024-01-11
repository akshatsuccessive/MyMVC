using Microsoft.EntityFrameworkCore;
using MyFirstMVCCore.Models.Domain;

namespace MyFirstMVCCore.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
