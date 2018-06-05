using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;

namespace BusinessLogic.Responses
{
    public class GetUserTeamsResponse : BaseResponse
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}
