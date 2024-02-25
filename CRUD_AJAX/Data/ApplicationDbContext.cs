using CRUD_AJAX.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CRUD_AJAX.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }  
    }
}
