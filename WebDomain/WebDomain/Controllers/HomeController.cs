using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.Requests;
using BusinessLogic.Responses;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using WebDomain.Common;
using WebDomain.Models;

namespace WebDomain.Controllers
{
    public class HomeController : Controller
    {
        private IServiceManager service;

        public HomeController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult LondonCrime()
        {
            using (var reader = new StreamReader(@"C:\data\london_crime_by_lsoa.csv"))
            {
                long count = 0;
                long defect = 0;
                List<LondonCrimeEntity> londonCrimeEntity = new List<LondonCrimeEntity>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count == 0)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        if (line != null)
                        {
                            var values = line.Split(',');
                            if(values.Length != 7) continue;
                         
                                londonCrimeEntity.Add(new LondonCrimeEntity
                                {
                                    borough = values[0],
                                    lsoa_code = values[1],
                                    major_category = values[2],
                                    minor_category = values[3],
                                    value = values[4],
                                    year = values[5],
                                    month = values[6]
                                });

                            if (londonCrimeEntity.Count == 100000)
                            {
                                var result = service.CreateCsv(londonCrimeEntity);

                                londonCrimeEntity.Clear();
                            }
                        }
                    }
                    count++;
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult TeamDashboard(int teamId)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");

                var request = new GetTeamByIdRequest { TeamId = teamId };
                var response = service.GetTeamById(request);
                var team = new TeamVM
                {
                    TeamId = teamId,
                    TeamName = response.Team.TeamName
                };

                var teamMembers = new List<TeamMemberVm>();
                foreach (var member in response.Members)
                {
                    teamMembers.Add(new TeamMemberVm
                    {
                        UserId = member.UserId,
                        UserName = member.UserName,
                        UserRole = member.UserRole
                    });
                }

                team.TeamMembers = teamMembers;

                return View(team);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult UserDashboard()
        {

            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.UserId = userId;
                var request = new GetUserTeamsRequest { UserId = userId };
                var response = service.GetUserTeams(request);
                if (response.Success)
                {
                    var teams = new List<TeamVM>();
                    foreach (var team in response.Teams)
                    {
                        teams.Add(new TeamVM
                        {
                            TeamId = team.TeamId,
                            TeamName = team.TeamName,
                            ProjectManagerId = team.ProjectManagerId,
                            ProductOwnerId = team.ProductOwnerId,
                            ScrumMasterId = team.ScrumMasterId
                        });
                    }

                    return View(teams);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult CreateTeam()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamVM newTeam)
        {
            if (!ModelState.IsValid)
                throw new Exception("Model invalid");
            int userId = 0;
            if (Int32.TryParse(HttpContext.Session.GetString("UserId"), out userId))
            {
                var request = new CreateTeamRequest
                {
                    Team = new Team
                    {
                        TeamName = newTeam.TeamName

                    },
                    UserId = userId
                };
                var response = service.CreateTeam(request);

                if (response.Success)
                {
                    return RedirectToAction("UserDashboard");
                }

                ViewBag.Error = response.ErrorMessage;
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserVm newUserAccountuser)
        {
            if (!ModelState.IsValid)
                throw new Exception("Model invalid");

            var request = new CreateUserRequest
            {
                User = new User
                {
                    FirstName = newUserAccountuser.FirstName,
                    Email = newUserAccountuser.Email,
                    LastName = newUserAccountuser.LastName,
                    Password = newUserAccountuser.Password,
                    UserName = newUserAccountuser.UserName
                }
            };

            var response = service.CreatUser(request);

            if (response.Success)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = response.ErrorMessage;

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserVm userAccount)
        {
            if (string.IsNullOrEmpty(userAccount.UserName))
            {
                ModelState.AddModelError("", "Please provide user name");
            }

            if (string.IsNullOrEmpty(userAccount.Password))
            {
                ModelState.AddModelError("", "Please provide password for user");
            }

            var request = new GetUserRequest
            {
                User = new User
                {
                    UserName = userAccount.UserName,
                    Password = userAccount.Password
                }
            };

            var response = service.GetUser(request);
            if (response.Success)
            {
                HttpContext.Session.SetString("UserId", response.UserId.ToString());
                HttpContext.Session.SetString("UserName", response.User.UserName);
                return RedirectToAction("UserDashboard");
            }

            ModelState.AddModelError("", response.ErrorMessage);

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
