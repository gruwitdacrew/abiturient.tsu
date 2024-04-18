using Application_Service.DBContext;
using Application_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application_Service.Services.Abiturient_Component
{
    public class Abiturients
    {
        private readonly AppDbContext _context;
        public Abiturients(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddProgram(string userId, string accessToken, string programId)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                int count = await _context.ApplicationPrograms.Where(p => p.id == userId).CountAsync();
                if (count >= Application.applicationProgramsQuantity) return new ConflictObjectResult(new ErrorResponse(409, $"Вы не можете добавить больше {Application.applicationProgramsQuantity} программ"));
                if (await _context.Programs.Where(p => p.id == programId).FirstOrDefaultAsync() == null) return new NotFoundObjectResult(new ErrorResponse(404, "Программы с таким id не существует"));
                if (await _context.ApplicationPrograms.Where(p => p.id == userId).Where(p => p.programId == programId).FirstOrDefaultAsync() != null) return new ConflictObjectResult(new ErrorResponse(409, "У вас уже добавлена эта программа"));
                
                await _context.ApplicationPrograms.AddAsync(new ApplicationProgram(abiturient.id, programId, count + 1));
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> DeleteProgram(string userId, string accessToken, string programId)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                if (await _context.ApplicationPrograms.Where(p => p.id == userId).Where(p => p.programId == programId).ExecuteDeleteAsync() < 1)
                {
                    return new NotFoundObjectResult(new ErrorResponse(404, "У вас нет добавленной программы с таким id"));
                }
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }

        public async Task<IActionResult> ChangeProgramPriority(string userId, string accessToken, string programId, int priority)
        {
            Abiturient abiturient = await _context.Abiturients.Where(p => p.id == userId).FirstOrDefaultAsync();
            if (accessToken == abiturient.accessToken)
            {
                List<ApplicationProgram> programs = await _context.ApplicationPrograms.Where(p => p.id == userId).OrderBy(p => p.priority).ToListAsync();
                if (programs.Where(p => p.programId == programId).FirstOrDefault() == null)
                {
                    return new NotFoundObjectResult(new ErrorResponse(404, "У вас нет добавленной программы с таким id"));
                }

                ApplicationProgram program = programs.Where(p => p.programId == programId).FirstOrDefault();

                programs.Remove(program);
                programs.Insert(priority, program);

                for (int i = 0; i < programs.Count; i++)
                {
                    programs[i].priority = i + 1;
                }

                _context.ApplicationPrograms.UpdateRange(programs);
                await _context.SaveChangesAsync();

                return new OkObjectResult(string.Empty);
            }
            else return new UnauthorizedObjectResult(string.Empty);
        }
    }
}
