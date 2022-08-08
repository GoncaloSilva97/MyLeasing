using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;

namespace MyLeasing.Common.Helperes
{
    public interface IConverterHelper
    {
        Owner ToOwner(OwnerViewModel model, string path, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);



        Lessee ToLessee(LesseeViewModel model, string path, bool isNew);

        LesseeViewModel ToLesseeViewModel(Lessee lessee);







    }
}
