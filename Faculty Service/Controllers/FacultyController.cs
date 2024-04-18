using Faculty_Service.Services.Compatibility_Component;
using Faculty_Service.Services.Faculty_Component;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Faculty_Service.Controllers
{
    [Route("api/faculty/[action]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly Faculties _facultyService;
        private readonly Compatibility _compatibility;

        public FacultyController(Faculties facultyService, Compatibility compatibility)
        {
            _facultyService = facultyService;
            _compatibility = compatibility;
        }


        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> programs([FromQuery] string?[] faculty_name,
                                                  [FromQuery] string?[] education_level,
                                                  [FromQuery] string? education_form,
                                                  [FromQuery] string? education_language,
                                                  [FromQuery] string? program_name,
                                                  [FromQuery] int page_number = 1,
                                                  [FromQuery] int page_size = 5)
        {
            return await _facultyService.GetPrograms(faculty_name, education_level, education_form, education_language, program_name, page_number, page_size);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/faculty/programs/{programId}/compatibility")]
        public async Task<IActionResult> checkCompatibility([FromRoute] string programId)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _compatibility.CheckCompatibility(userId, accessToken, programId);
        }


        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> import()
        {
            return await _facultyService.Import();
        }
    }
}
