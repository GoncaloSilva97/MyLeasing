

using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data.Entities;
using System.Linq;

namespace MyLeasing.Common.Data
{
    public class LesseeRepository : GenericRepository<Lessee>, ILesseeRepository
    {

        public readonly DataContext _context;

        public LesseeRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public IQueryable GetAllLessee()
        {
            return _context.Lessee;
        }



    }
}
