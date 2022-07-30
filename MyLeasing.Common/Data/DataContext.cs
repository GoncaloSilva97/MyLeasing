using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data.Entities;

namespace MyLeasing.Common.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
