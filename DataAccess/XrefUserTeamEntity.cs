using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class XrefUserTeamEntity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual TeamEntity Team { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
