using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Common.Data.Entities
{
    public class Owner : IEntity
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

        [Display(Name = "Owner Name")]
        public string FullName => $"{FirstName} {LastName}";


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Fixed Phone")]
        public double FixPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public double CellPhone { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "The filde {0} can contain {1} Characteres length.")]
        [Display(Name = "Address")]
        public string Address { get; set; }



        public User User { get; set; }



        



        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://myleasinggs.azurewebsites.net{ImageUrl.Substring(1)}";
            }
        }






    }
}
