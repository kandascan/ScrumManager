using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TeamEntity
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int? ScrumMasterId { get; set; }
        public int? ProjectManagerId { get; set; }
        public int? ProductOwnerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<XrefUserTeamEntity> XrefUsersTeam { get; set; }

    }
}
