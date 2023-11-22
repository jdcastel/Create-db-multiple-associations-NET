using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JDRA5.Models
{
    public class EpisodeAddFormViewModel : EpisodeAddViewModel
    {
        [Display(Name = "Genres")]
        public SelectList GenreList { get; set; }
    }
}