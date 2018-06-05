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
        public string ScrumMaster { get; set; }
        public string ProductManager { get; set; }
        public string ProductOwner { get; set; }
        public IEnumerable<UserVm> TeamMembers { get; set; }
    }
}
