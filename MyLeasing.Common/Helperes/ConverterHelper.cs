

using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;

namespace MyLeasing.Common.Helperes
{
    public class ConverterHelper : IConverterHelper
    {
        public Lessee ToLessee(LesseeViewModel model, string path, bool isNew)
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                PhotoUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixPhone = model.FixPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                Document = model.Document

            };
        }

        public LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel
            {
                Id = lessee.Id,
                PhotoUrl = lessee.PhotoUrl,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixPhone = lessee.FixPhone,
                CellPhone = lessee.CellPhone,
                Address = lessee.Address,
                Document = lessee.Document

            };
        }






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
