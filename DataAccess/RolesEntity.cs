using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class RolesEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}

