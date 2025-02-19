using Crud_aplicationwebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_aplicationwebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
