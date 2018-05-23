using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace DataAccess
{
    public class User
    {
        public int Id  { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
