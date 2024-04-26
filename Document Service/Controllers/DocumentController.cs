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

            return await _educationService.AddEducationDocument(userId, educationDocument);
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

            return await _educationService.GetEducationDocument(userId);
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

            return await _educationService.EditEducationDocument(userId, educationDocument);
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

            return await _educationService.DeleteEducationDocument(userId);
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

            return await _passportService.AddPassportDocument(userId, passportDocumentRequest);
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

            return await _passportService.GetPassportDocument(userId);
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

            return await _passportService.EditPassportDocument(userId, passportDocument);
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

            return await _passportService.DeletePassportDocument(userId);
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

            return await _scanService.UploadScanFile(userId, document_type.ToString(), file);
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

            return await _scanService.GetScanFile(userId, document_type.ToString());
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

            return await _scanService.DeleteScanFile(userId, document_type.ToString());
        }
    }
}
