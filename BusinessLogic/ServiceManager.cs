using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Requests;
using BusinessLogic.Responses;
using DataAccess;

namespace BusinessLogic
{
    public class ServiceManager : IServiceManager
    {
        private IUnitOfWork uow;

        public ServiceManager()
        {
            this.uow = new UnitOfWork();
        }

        public CreateTeamResponse CreateTeam(CreateTeamRequest request)
        {
            var response = new CreateTeamResponse();

            var dbTeam = new TeamEntity
            {
                TeamName = request.Team.TeamName,
                ProjectManagerId = request.UserId
            };

            try
            {
                uow.Repository<TeamEntity>().Add(dbTeam);
                uow.Save();

                var dbXrefUserTeam = new XrefUserTeamEntity
                {
                    TeamId = dbTeam.TeamId,
                    UserId = request.UserId
                };
                uow.Repository<XrefUserTeamEntity>().Add(dbXrefUserTeam);
                uow.Save();

                response.Success = true;
                response.TeamId = dbTeam.TeamId;
                response.UserId = request.UserId;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public CreateUserResponse CreatUser(CreateUserRequest request)
        {
            string encryptedPassword = HashPassword(request.User.Password);
            var response = new CreateUserResponse();

            var dbUser = new UserEntity
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = request.User.Email,
                Password = encryptedPassword,
                Username = request.User.UserName
            };

            try
            {
                uow.Repository<UserEntity>().Add(dbUser);
                uow.Save();
                response.Success = true;
                response.UserId = dbUser.UserId;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public GetUserTeamsResponse GetUserTeams(GetUserTeamsRequest request)
        {
            var response = new GetUserTeamsResponse();

            try
            {
                //var dbUsers = uow.Repository<UserEntity>().GetOverview();
                var dbTeams = uow.Repository<TeamEntity>().GetOverview();
                var dbXrefUsersTeam = uow.Repository<XrefUserTeamEntity>().GetOverview();

                var userTeams = (from xref in dbXrefUsersTeam
                                     //join user in dbUsers
                                     //  on xref.UserId equals user.UserId
                                 join team in dbTeams
                                     on xref.TeamId equals team.TeamId
                                 where xref.UserId == request.UserId
                                 select new Team
                                 {
                                     TeamId = team.TeamId,
                                     TeamName = team.TeamName,
                                     ProjectManagerId = team.ProjectManagerId
                                 }
                    ).ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var dbUser = uow.Repository<UserEntity>().GetDetails(x => x.Username == request.User.UserName);
                if (dbUser == null)
                {
                    throw new Exception("User doesn't exist in database");
                }

                string encryptedPassword = HashPassword(request.User.Password);
                if (encryptedPassword != dbUser.Password)
                {
                    //TODO: later try login count additional column in db and increase each time when password is wrong.
                    throw new Exception("Password is incorrect");
                }

                response.User = new User
                {
                    UserName = dbUser.Username,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };

                response.Success = true;
                response.UserId = dbUser.UserId;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        private string HashPassword(string password)
        {
            byte[] hash;
            var data = Encoding.UTF8.GetBytes(password);

            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }

            StringBuilder stringBuilder = new StringBuilder(128);

            foreach (var b in hash)
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
