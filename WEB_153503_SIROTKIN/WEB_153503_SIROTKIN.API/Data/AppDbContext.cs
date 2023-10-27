using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.Domain.Entities;

namespace WEB_153503_SIROTKIN.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
    }
}
