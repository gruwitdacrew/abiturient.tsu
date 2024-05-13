using Application_Service.Models;
using Application_Service.Models.Requests;
using Application_Service.Services.Abiturient_Component;
using Document_Service.Services;
using EasyNetQ;
using Manager_Panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Manager_Panel.Controllers
{
    public class ApplicationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Applications(string fullName = null, string[] faculty_names = null, string status = null, string program_name = null, bool hasManager = false, bool myAbiturients = false, bool lastModifiedAsc = true, string managerId = null, int page_current = 1, int page_size = 10)
        {
            //if (TempData["RefreshToken"] == null) return RedirectToAction("Login", "Account");

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                IActionResult response = await bus.Rpc.RequestAsync<ApplicationsRequest, IActionResult>(new ApplicationsRequest {
                    fullName = fullName,
                    faculty_names = faculty_names,
                    status = status,
                    program_name = program_name,
                    hasManager = hasManager,
                    myAbiturients = myAbiturients,
                    lastModifiedAsc = lastModifiedAsc,
                    managerId = managerId,
                    page_current = page_current,
                    page_size = page_size
                });

                if (response is OkObjectResult okResult)
                {
                    var applicationsResponse = (ApplicationsResponse)okResult.Value;
                    return View(applicationsResponse);
                }
                else
                {
                    var errorResponse = (ErrorResponse)((ObjectResult)response).Value;
                    return View("~/Views/Error/Error.cshtml", errorResponse);
                }

            }
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