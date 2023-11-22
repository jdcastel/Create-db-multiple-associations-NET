using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JDRA5.Models
{
    public class ShowWithInfoViewModel : ShowBaseViewModel
    {
        public ICollection<ActorBaseViewModel> Actors { get; set; }
        public ICollection<EpisodeBaseViewModel> Episodes { get; set; }
    }
}