using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using WebDomain.Models;

namespace WebDomain.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = null;
        private IRepository<User> repository;

        public HomeController()
        {
            uow = new UnitOfWork();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var test = uow.Repository<User>().GetDetails(x => x.Id == 1);
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if(!ModelState.IsValid)
                throw new Exception("Model invalid");

            uow.Repository<User>().Add(user);
            uow.SaveChanges();

            return Redirect("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (!ModelState.IsValid)
                throw new Exception("Model invalid");

            var userdb = uow.Repository<User>().GetDetails(u => u.Username == user.Username && u.Password == user.Password);
            if (userdb != null && userdb.Active == true)
            {
                return Redirect("Home/Index");
            }

            return View();

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
