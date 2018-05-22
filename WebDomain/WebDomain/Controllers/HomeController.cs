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

        public HomeController()
        {
            uow = new UnitOfWork();
        }
        public IActionResult Index()
        {
            var test = uow.Repository<User>().GetDetails(x => x.Id == 1);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
