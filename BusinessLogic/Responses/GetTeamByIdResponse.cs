using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.Responses
{
    public class GetTeamByIdResponse : BaseResponse
    {
        public Team Team { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
