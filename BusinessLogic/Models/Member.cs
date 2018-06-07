using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.Models
{
    public class Member
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; internal set; }
    }
}
