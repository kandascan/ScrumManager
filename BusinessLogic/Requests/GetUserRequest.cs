using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Requests;

namespace BusinessLogic.Requests
{
    public class GetUserRequest : BaseRequest
    {
        public User User { get; set; }
    }
}
