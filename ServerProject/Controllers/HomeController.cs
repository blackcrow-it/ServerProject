using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerProject.Models;

namespace ServerProject.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    using ServerProject.Security;

    public class HomeController : Controller

    {
        private readonly ServerProjectContext _context;

        public HomeController(ServerProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Accounts account, string Url)
        {
            var existAccount = _context.Accounts.SingleOrDefault(a => a.UserName == account.UserName);
            
            if (existAccount != null)
            {

                if (existAccount.Password == Handlepassword.GetInstance().EncryptPassword(account.Password, existAccount.Salt))
                {
                    HttpContext.Session.SetString("currentLogin", existAccount.UserName);
                    HttpContext.Session.SetString("currentLoginId", existAccount.Id.ToString());
                    
                    HttpContext.Session.SetString("currentLoginRole", existAccount.Role.ToString());
                   
                    return Redirect("/Grades/Index");
                }
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("currentLogin");
            HttpContext.Session.Remove("currentLoginId");
            HttpContext.Session.Remove("currentLoginRole");
            return Redirect("/Home/Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = HttpContext.Session.GetString("currentLogin");

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
