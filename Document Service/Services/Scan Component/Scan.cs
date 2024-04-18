using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document_Service.Models;
using Document_Service.DBContext;

namespace Document_Service.Services
{
    public class Scan
    {
        private readonly AppDbContext _context;
        public Scan(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<IActionResult> UploadScanFile(string userId, string accessToken, string document_type, IFormFile file)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {

                if (file == null || file.Length == 0)
                    return new BadRequestObjectResult(new ErrorResponse(400, "Файл не выбран"));

                if (file.ContentType != "application/pdf")
                    return new BadRequestObjectResult(new ErrorResponse(400, "Файл должен быть в формате pdf"));

                byte[] pdfFileData;

                if (document_type == "passport")
                {
                    PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        pdfFileData = memoryStream.ToArray();
                    }
                    passportDocument.scan = pdfFileData;

                    _context.Passports.Update(passportDocument);
                }
                else
                {
                    EducationDocument educationDocument = await _context.Educations.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об обрзовании"));

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        pdfFileData = memoryStream.ToArray();
                    }
                    educationDocument.scan = pdfFileData;

                    _context.Educations.Update(educationDocument);
                }
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> GetScanFile(string userId, string accessToken, string document_type)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();

            if (accessToken == abiturient.accessToken)
            {
                FileContentResult file;

                if (document_type == "passport")
                {
                    PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));
                    if (passportDocument.scan == null) return new NotFoundObjectResult(new ErrorResponse(400, "У вас нет прикрепленного скана паспорта"));

                    file = new FileContentResult(passportDocument.scan, "application/pdf")
                    {
                        FileDownloadName = "passport.pdf"
                    };

                    return file;
                }
                else
                {
                    EducationDocument educationDocument = await _context.Educations.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об образовании"));
                    if (educationDocument.scan == null) return new NotFoundObjectResult(new ErrorResponse(400, "У вас нет прикрепленного скана документа об образовании"));

                    file = new FileContentResult(educationDocument.scan, "application/pdf")
                    {
                        FileDownloadName = "education.pdf"
                    };

                    return file;
                }
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> DeleteScanFile(string userId, string accessToken, string document_type)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();

            if (accessToken == abiturient.accessToken)
            {
                if (document_type == "passport")
                {
                    PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));
                    if (passportDocument.scan == null) return new NotFoundObjectResult(new ErrorResponse(400, "У вас нет прикрепленного скана паспорта"));

                    passportDocument.scan = null;

                    _context.Passports.Update(passportDocument);
                }
                else
                {
                    EducationDocument educationDocument = await _context.Educations.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                    if (educationDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали документ об образовании"));
                    if (educationDocument.scan == null) return new NotFoundObjectResult(new ErrorResponse(400, "У вас нет прикрепленного скана документа об образовании"));

                    educationDocument.scan = null;

                    _context.Educations.Update(educationDocument);
                }

                await _context.SaveChangesAsync();
                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

    }
}
