using EasyNetQ;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Users_Service.Models;

namespace Manager_Panel.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                IActionResult response = await bus.Rpc.RequestAsync<LoginRequest, IActionResult> (new LoginRequest { email = email, password = password });

                if (response is OkObjectResult okResult)
                {
                    var tokenResponse = (TokenResponse)okResult.Value;

                    ViewBag.AccessToken = tokenResponse.accessToken;
                    ViewBag.RefreshToken = tokenResponse.refreshToken;
                    ViewBag.Message = "Привет, мир!";

                    return RedirectToAction("Applications", "Application");
                }
                else if (response is ObjectResult objectResult)
                {
                    var errorResponse = (ErrorResponse)objectResult.Value;
                    ViewBag.ErrorMessage = errorResponse.message;
                    return RedirectToAction("Login", "Account"); // Перенаправление на другую страницу после успешного входа;
                }
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
