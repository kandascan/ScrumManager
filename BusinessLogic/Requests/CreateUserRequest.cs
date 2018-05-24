using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;

namespace BusinessLogic.Requests
{
    public class CreateUserRequest : BaseRequest
    {
       public User User { get; set; } 
    }
}
