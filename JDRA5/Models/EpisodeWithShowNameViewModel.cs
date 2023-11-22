using JDRA5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JDRA5.Models
{
    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
    {
        [Display(Name = "Show Name")]
        public Show Show { get; set; }
    }
}