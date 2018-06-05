using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
        public int TeamId { get; set; }
    }
}
