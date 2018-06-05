using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Models;

namespace BusinessLogic.Requests
{
    public class CreateTeamRequest : BaseRequest
    {
        public Team Team { get; set; }
    }
}
