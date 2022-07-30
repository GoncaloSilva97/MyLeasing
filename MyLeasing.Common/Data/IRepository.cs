using MyLeasing.Common.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLeasing.Data
{
    public interface IRepository
    {
        void AddOwner(Owner owner);
        IEnumerable<Owner> GetOwner();
        Owner GetOwner(int id);
        bool OwnerExists(int id);
        void RemoveOwner(Owner owner);
        Task<bool> SaveAllAsync();
        void UpdateOwner(Owner owner);
    }
}