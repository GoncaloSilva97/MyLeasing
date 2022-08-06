

using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;

namespace MyLeasing.Common.Helperes
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner ToOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixPhone = model.FixPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                Document = model.Document,
                User = model.User
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                ImageUrl = owner.ImageUrl,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixPhone = owner.FixPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                Document = owner.Document,
                User = owner.User

            };
        }
    }
}
