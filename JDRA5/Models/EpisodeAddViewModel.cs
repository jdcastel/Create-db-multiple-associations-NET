using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Models
{
    public class EpisodeAddViewModel
    {
        public string Name { get; set; }
        public string ShowName { get; set; }
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AirDate { get; set; } = DateTime.Now;
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public string Clerk { get; set; }
        public int ShowId { get; set; }
    }
}