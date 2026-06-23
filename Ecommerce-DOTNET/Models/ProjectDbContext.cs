using Microsoft.EntityFrameworkCore;

namespace Ecommerce_DOTNET.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
