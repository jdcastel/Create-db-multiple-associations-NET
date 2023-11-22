using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Data
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The field cannot exceed 50 characters")]
        public string Name { get; set; }
        public ICollection<Show> Shows { get; set; }

    }
}