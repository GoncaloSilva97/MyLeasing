using Microsoft.AspNetCore.Http;
using MyLeasing.Common.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Models
{
    public class LesseeViewModel : Lessee
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
