using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;
using System;

namespace MyLeasing.Common.Helperes
{
    public interface IConverterHelper
    {
        Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);



        Lessee ToLessee(LesseeViewModel model, Guid photoId, bool isNew);

        LesseeViewModel ToLesseeViewModel(Lessee lessee);







    }
}
