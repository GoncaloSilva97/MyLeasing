

using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data.Entities;
using System.Linq;

namespace MyLeasing.Common.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {

        public readonly DataContext _context;

        public OwnerRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(p => p.User);
        }



    }
}
