using Microsoft.EntityFrameworkCore;
using UserPage.Models.Entities;

namespace UserPage.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users{ get; set; }
    }
    
}
