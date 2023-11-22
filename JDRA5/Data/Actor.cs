using JDRA5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Data
{
    public class Actor
    {
        public Actor()
        {
            Shows = new HashSet<Show>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The field cannot exceed 150 characters")]
        public string Name { get; set; }

        [StringLength(150, ErrorMessage = "The field cannot exceed 150 characters")]
        public string AlternateName { get; set; }

        public DateTime? BirthDate { get; set; }

        public double? Height { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string Executive { get; set; }

        //Associations
        public ICollection<Show> Shows { get; set; }
    }
}