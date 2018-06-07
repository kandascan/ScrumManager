using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDomain.Models
{
    public class TeamVM
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        public string TeamName { get; set; }
        public int? ScrumMasterId { get; set; }
        public int? ProjectManagerId { get; set; }
        public int? ProductOwnerId { get; set; }
        public string ScrumMaster { get; set; }
        public string ProjectManager { get; set; }
        public string ProductOwner { get; set; }
        public IEnumerable<TeamMemberVm> TeamMembers { get; set; }
    }
}
