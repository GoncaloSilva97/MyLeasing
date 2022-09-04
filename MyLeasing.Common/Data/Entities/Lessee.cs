using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Common.Data.Entities
{
    public class Lessee : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }



        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";







        [Display(Name = "Photo")]
        public Guid PhotoId { get; set; }














        [Display(Name = "Fixed Phone")]
        public double FixPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public double CellPhone { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "The filde {0} can contain {1} Characteres length.")]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "Full Name With Document")]
        public string FullNameWithDocument => $"{FirstName} {LastName} {Document}";

        public User User { get; set; }


        public string PhotoFullPath => PhotoId == Guid.Empty
            ? $"https://myleasinggs.azurewebsites.net/image/lessee/noimage.png"
            : $"https://myleasinggs.blob.core.windows.net/lessees/{PhotoId}";



       



    }
}
