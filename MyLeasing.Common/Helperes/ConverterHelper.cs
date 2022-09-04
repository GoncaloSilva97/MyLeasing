

using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;
using System;

namespace MyLeasing.Common.Helperes
{
    public class ConverterHelper : IConverterHelper
    {
        public Lessee ToLessee(LesseeViewModel model, Guid photoId, bool isNew)
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                PhotoId = photoId,
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
                PhotoId = lessee.PhotoId,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixPhone = lessee.FixPhone,
                CellPhone = lessee.CellPhone,
                Address = lessee.Address,
                Document = lessee.Document

            };
        }






        public Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
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
                ImageId = owner.ImageId,
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
