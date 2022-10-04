using BulkyBooks1.Models;

using Microsoft.EntityFrameworkCore;

namespace BulkyBooks1.DataAccessLayer
{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base (options)
        {
                    
        }
        public DbSet<Category> Categories { get; set; }
    }
}
