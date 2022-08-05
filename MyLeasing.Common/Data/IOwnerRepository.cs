using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Common.Data
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        public IQueryable GetAllWithUsers();
    }
}
