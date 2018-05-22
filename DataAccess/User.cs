using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id  { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }
}
