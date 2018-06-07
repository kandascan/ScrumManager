using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace DataAccess
{
    public class UserEntity
    {
        public int UserId  { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public virtual ICollection<XrefUserTeamEntity> XrefUsersTeam { get; set; }
        public virtual RolesEntity Role { get; set; }

    }
}
