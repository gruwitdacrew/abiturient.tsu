using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Document_Service.Models;
using Document_Service.DBContext;
using Microsoft.EntityFrameworkCore;
using EasyNetQ;

namespace Document_Service.Services
{
    public class Education
    {
        private readonly AppDbContext _context;

        public Education(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<EducationDocumentResponse>> GetEducationDocumentTypes(string userId)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                List<string> response = await bus.Rpc.RequestAsync<string, List<string>>("");
                return new OkObjectResult(response);
            }
        }

        public async Task<IActionResult> AddEducationDocument(string userId, EducationDocumentRequest educationDocumentRequest)
        {
            if (await _context.Educations.Where(p => p.id == userId).FirstOrDefaultAsync() != null) return new ConflictObjectResult(new ErrorResponse(409, "У вас уже указан документ об образовании"));

            await _context.Educations.AddAsync(new EducationDocument(userId, educationDocumentRequest));
            await _context.SaveChangesAsync();

            return new OkObjectResult(string.Empty);
        }

        public async Task<ActionResult<EducationDocumentResponse>> GetEducationDocument(string userId)
        {
            EducationDocument educationDocument = await _context.Educations.Where(p => p.id == userId).FirstOrDefaultAsync();

            if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об образовании"));

            return new OkObjectResult(new EducationDocumentResponse(educationDocument));
        }

        public async Task<IActionResult> EditEducationDocument(string userId, EducationDocumentRequest educationDocumentRequest)
        {
            EducationDocument educationDocument = await _context.Educations.Where(p => p.id == userId).FirstOrDefaultAsync();

            if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об образовании"));

            if (educationDocumentRequest.number != null)
            {
                educationDocument.number = educationDocumentRequest.number;
            }
            if (educationDocumentRequest.documentType != null)
            {
                educationDocument.documentType = educationDocumentRequest.documentType;
            }
            if (educationDocumentRequest.grade != null)
            {
                educationDocument.grade = educationDocumentRequest.grade;
            }
            if (educationDocumentRequest.date != null)
            {
                educationDocument.date = educationDocumentRequest.date;
            }

            _context.Educations.Update(educationDocument);
            await _context.SaveChangesAsync();

            return new OkObjectResult(string.Empty);
        }

        public async Task<IActionResult> DeleteEducationDocument(string userId)
        {
            EducationDocument educationDocument = await _context.Educations.Where(p => p.id == userId).FirstOrDefaultAsync();

            if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об образовании"));

            await _context.Educations.Where(p => p.id == educationDocument.id).ExecuteDeleteAsync();

            return new OkObjectResult(string.Empty);
        }

    }
}
