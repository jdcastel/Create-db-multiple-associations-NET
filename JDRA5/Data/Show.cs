using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Data
{
    public class Show
    {
        public Show()
        {
            Actors = new HashSet<Actor>();
            Episodes = new HashSet<Episode>();
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The field cannot exceed 150 characters")]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The field cannot exceed 250 characters")]
        public string Coordinator { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The field cannot exceed 50 characters")]
        public string Genre { get; set; }

        //Associations
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Episode> Episodes { get; set; }

    }
}