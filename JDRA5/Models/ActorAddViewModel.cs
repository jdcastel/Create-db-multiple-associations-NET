using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Models
{
    public class ActorAddViewModel
    {
        [Required]
        public string Name { get; set; }

        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        public double? Height { get; set; }

        [Display(Name = "Image")]
        [Required]
        public string ImageUrl { get; set; }

        public string Executive { get; set; }
    }
}