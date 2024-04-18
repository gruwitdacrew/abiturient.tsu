using EasyNetQ;
using Faculty_Service.DBContext;
using Faculty_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Faculty_Service.Services.Compatibility_Component
{
    public class Compatibility
    {
        private readonly AppDbContext _context;
        public Compatibility(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CheckCompatibility(string userId, string accessToken, string programCode)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                var nextLevelId = await _context.Programs.Where(x => x.code == programCode).Select(x => x.levelId).FirstOrDefaultAsync();

                var levelIds = await _context.NextLevels.Where(x => x.nextLevelId == nextLevelId).Select(x => x.levelId).ToListAsync();

                var documentTypes = await _context.EducationDocumentTypes.Where(x => levelIds.Contains(x.levelId)).Select(x => x.name).ToListAsync();

                using (var bus = RabbitHutch.CreateBus("host=localhost"))
                {
                    bool response = await bus.Rpc.RequestAsync<CompatibilityCheckRequest, bool>(new CompatibilityCheckRequest(userId, documentTypes));
                    return new OkObjectResult(response);
                }
            }
            return new UnauthorizedObjectResult(string.Empty);
        }
    }
}
