using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data.Entities;

namespace SuperShopGS.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
