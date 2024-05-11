using Application_Service.DBContext;
using Application_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application_Service.Services.Abiturient_Component
{
    public class Managers
    {
        private readonly AppDbContext _context;
        public Managers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetApplications(string fullName,
                                                             string[]? faculty_names,
                                                             string status,
                                                             string program_name,
                                                             string hasManager,
                                                             string myAbiturients,
                                                             string lastModified,
                                                             int page_current,
                                                             int page_size)
        {
            if (page_current < 1 || page_size < 1) return new BadRequestObjectResult(new ErrorResponse(400, "Page_current и page_size должны быть положительными числами"));

            var query = _context.Applications.AsQueryable();

            if (faculty_names.Any() && faculty_names[0] != null)
            {
                var applicationIds = _context.ApplicationPrograms.Where(x => faculty_names.ToList().Contains(x.educationProgram.name)).Select(x => x.id);

                query = query.Where(x => _context.Applications.Where(x => applicationIds.ToList().Contains(x.id)));
            }

            if (fullName != null)
            {
                query = query.Where(x => x.abiturient.fullName.Contains(fullName));
            }

            if (education_language != null)
            {
                query = query.Where(x => x.language == education_language);
            }

            if (program_name != null)
            {
                query = query.Where(x => x.StartsWith(program_name));
            }

            int count = (int)Math.Ceiling((double)await query.CountAsync() / page_size);
            if (page_current > count) { return new BadRequestObjectResult(new ErrorResponse(400, "Page_current не может быть больше количества страниц")); }

            var programs = await query.Join(_context.Levels, program => program.levelId, level => level.id, (program, level) => new { EducationPrograms = program, Levels = level }).Join(_context.Faculties, programAndLevels => programAndLevels.EducationPrograms.facultyId, faculty => faculty.id, (program, faculty) => new EducationProgramResponse(program.EducationPrograms, faculty.name, program.Levels.name)).Skip((page_current - 1) * page_size).Take(page_size).ToListAsync();


            return new OkObjectResult(new ProgramsResponse(programs, new Pagination(page_size, count, page_current)));
        }
    }
}
