using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? ScrumMasterId { get; set; }
        public int? ProjectManagerId { get; set; }
        public int? ProductOwnerId { get; set; }
    }
}
