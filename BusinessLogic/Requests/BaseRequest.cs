using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.Requests
{
    public class BaseRequest
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
