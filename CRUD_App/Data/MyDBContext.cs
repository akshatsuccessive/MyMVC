using CRUDApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDApplication.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
