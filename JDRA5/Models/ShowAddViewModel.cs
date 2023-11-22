using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Models
{
    public class ShowAddViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public string Coordinator { get; set; }
        public int ShowId { get; set; }
        public int ActorId { get; set; }
        public int GenreId { get; set; }
        public string ShowName { get; set; }
        public IEnumerable<int> SelectedActorIds { get; set; }
    }
}