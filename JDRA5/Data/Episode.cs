using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Data
{
    public class Episode
    {
        public Episode()
        {
            AirDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The field cannot exceed 150 characters")]
        public string Name { get; set; }

        [Required]
        public int SeasonNumber { get; set; }

        [Required]
        public int EpisodeNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The field cannot exceed 50 characters")]
        public string Genre { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string Clerk { get; set; }

        //Associations
        [Required]
        public int ShowId { get; set; }
        public Show Show { get; set; }

    }
}