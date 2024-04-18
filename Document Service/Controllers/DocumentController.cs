using Document_Service.Models;
using Document_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Text.Json.Serialization;

namespace Document_Service.Controllers
{
    [Route("api/document/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly Scan _scanService;
        private readonly Passport _passportService;
        private readonly Education _educationService;

        public DocumentController(Education educationService, Passport passportService, Scan scanService)
        {
            _scanService = scanService;
            _passportService = passportService;
            _educationService = educationService;
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/education")]
        public async Task<IActionResult> addEducationDocument([FromBody] EducationDocumentRequest educationDocument)
        {
            //try
            //{
            //    await _documentValidator.Validate(educationDocument);
            //}
            //catch (ArgumentException ex)
            //{
            //    return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            //}
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _educationService.AddEducationDocument(userId, accessToken, educationDocument);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/education")]
        public async Task<ActionResult<EducationDocumentResponse>> getEducationDocument()
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _educationService.GetEducationDocument(userId, accessToken);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/education")]
        public async Task<IActionResult> editEducationDocument([FromBody] EducationDocumentRequest educationDocument)
        {
            //try
            //{
            //    await _documentValidator.Validate(educationDocument);
            //}
            //catch (ArgumentException ex)
            //{
            //    return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            //}
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _educationService.EditEducationDocument(userId, accessToken, educationDocument);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/education")]
        public async Task<IActionResult> deleteEducationDocument()
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _educationService.DeleteEducationDocument(userId, accessToken);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/passport")]
        public async Task<IActionResult> addPassportDocument([FromBody] PassportDocumentRequest passportDocumentRequest)
        {
            //try
            //{
            //    await _documentValidator.Validate(educationDocument);
            //}
            //catch (ArgumentException ex)
            //{
            //    return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            //}
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _passportService.AddPassportDocument(userId, accessToken, passportDocumentRequest);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/passport")]
        public async Task<ActionResult<PassportDocumentResponse>> getPassportDocument()
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _passportService.GetPassportDocument(userId, accessToken);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/passport")]
        public async Task<IActionResult> editEducationDocument([FromBody] PassportDocumentRequest passportDocument)
        {
            //try
            //{
            //    await _documentValidator.Validate(educationDocument);
            //}
            //catch (ArgumentException ex)
            //{
            //    return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            //}
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _passportService.EditPassportDocument(userId, accessToken, passportDocument);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/passport")]
        public async Task<IActionResult> deletePassportDocument()
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _passportService.DeletePassportDocument(userId, accessToken);
        }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Document_type
        {
            passport,
            education
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/{document_type}/scan")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> uploadScanFile([FromRoute] Document_type document_type, IFormFile file)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _scanService.UploadScanFile(userId, accessToken, document_type.ToString(), file);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/{document_type}/scan")]
        public async Task<IActionResult> getScanFile([FromRoute] Document_type document_type)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _scanService.GetScanFile(userId, accessToken, document_type.ToString());
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        [Route("/api/document/{document_type}/scan")]
        public async Task<IActionResult> deleteScanFile([FromRoute] Document_type document_type)
        {
            var userId = User.Claims.ToList()[0].Value;
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _scanService.DeleteScanFile(userId, accessToken, document_type.ToString());
        }
    }
}
