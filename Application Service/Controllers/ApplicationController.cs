using Application_Service.Services.Abiturient_Component;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Application_Service.Controllers
{
    [Route("api/application/[action]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly Abiturients _abiturientService;

        public ApplicationController(Abiturients abiturientService)
        {
            _abiturientService = abiturientService;
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/application/program/{programId}")]
        public async Task<IActionResult> addProgram([FromRoute] string programId)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _abiturientService.AddProgram(userId, accessToken, programId);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/application/program/{programId}")]
        public async Task<IActionResult> deleteProgram([FromRoute] string programId)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _abiturientService.DeleteProgram(userId, accessToken, programId);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/application/program/{programId}/priority")]
        public async Task<IActionResult> changeProgramPriority([FromRoute] string programId, int priority)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _abiturientService.ChangeProgramPriority(userId, accessToken, programId, priority);
        }
    }
}
