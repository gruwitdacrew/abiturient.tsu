using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document_Service.Models;
using Document_Service.DBContext;

namespace Document_Service.Services
{
    public class Passport
    {
        private readonly AppDbContext _context;
        public Passport(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddPassportDocument(string userId, string accessToken, PassportDocumentRequest passportDocumentRequest)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                if (await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync() != null) return new ConflictObjectResult(new ErrorResponse(409, "У вас уже указан паспорт"));

                await _context.Passports.AddAsync(new PassportDocument(abiturient, passportDocumentRequest));
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<ActionResult<PassportDocumentResponse>> GetPassportDocument(string userId, string accessToken)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));

                return new OkObjectResult(new PassportDocumentResponse(passportDocument));
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> EditPassportDocument(string userId, string accessToken, PassportDocumentRequest passportDocumentRequest)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));

                if (passportDocumentRequest.number != null)
                {
                    passportDocument.number = passportDocumentRequest.number;
                }
                if (passportDocumentRequest.series != null)
                {
                    passportDocument.series = passportDocumentRequest.series;
                }
                if (passportDocumentRequest.date != null)
                {
                    passportDocument.date = passportDocumentRequest.date;
                }
                if (passportDocumentRequest.date != null)
                {
                    passportDocument.date = passportDocumentRequest.date;
                }

                _context.Passports.Update(passportDocument);
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> DeletePassportDocument(string userId, string accessToken)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                PassportDocument passportDocument = await _context.Passports.Where(p => p.id == abiturient.id).FirstOrDefaultAsync();

                if (passportDocument == null) return new NotFoundObjectResult(new ErrorResponse(404, "Вы не указывали паспорт"));

                await _context.Passports.Where(p => p.id == passportDocument.id).ExecuteDeleteAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

    }
}
