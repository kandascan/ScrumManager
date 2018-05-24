using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.Requests;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserAccount newUserAccountuser)
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
        public IActionResult Login(UserAccount userAccount)
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
                return RedirectToAction("Index");
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
