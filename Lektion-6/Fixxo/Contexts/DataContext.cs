using Fixxo.Models;
using Microsoft.EntityFrameworkCore;

namespace Fixxo.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
