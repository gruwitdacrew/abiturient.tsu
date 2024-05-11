using EasyNetQ;
using Manager_Panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Users_Service.Models;

namespace Manager_Panel.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Applications()
        {
            return View();
        }

        public IActionResult Application(string id)
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