﻿using BusinessLogic.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BusinessLogic.Requests;

namespace BusinessLogic
{
    public interface IServiceManager
    {
        CreateUserResponse CreatUser(CreateUserRequest request);
        GetUserResponse GetUser(GetUserRequest request);
        CreateTeamResponse CreateTeam(CreateTeamRequest request);
        GetUserTeamsResponse GetUserTeams(GetUserTeamsRequest request);
        GetTeamByIdResponse GetTeamById(GetTeamByIdRequest request);
    }
}
