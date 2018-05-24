using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;

namespace BusinessLogic.Responses
{
    public class GetUserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}
