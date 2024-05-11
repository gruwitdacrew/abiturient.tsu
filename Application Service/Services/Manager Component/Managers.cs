using Application_Service.DBContext;
using Application_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                                                             string[]? faculty_name,
                                                             string status,
                                                             string hasManager,
                                                             string myAbiturients,
                                                             string lastModified,
                                                             int page_current,
                                                             int page_size)
        {
            if (page_current < 1 || page_size < 1) return new BadRequestObjectResult(new ErrorResponse(400, "Page_current и page_size должны быть положительными числами"));

            var query = _context.Applications.AsQueryable();

            return new OkObjectResult(query);

            //if (faculty_name.Any() && faculty_name[0] != null)
            //{
            //    List<string> faculties = await _context.Applications.Where(x => faculty_name.Contains(x.)).Select(x => x.id).ToListAsync();
            //    query = query.Where(x => faculties.Contains(x.facultyId));
            //}

            //if (education_level.Any() && education_level[0] != null)
            //{
            //    List<string> levels = await _context.Levels.Where(x => education_level.Contains(x.name)).Select(x => x.id).ToListAsync();
            //    query = query.Where(x => levels.Contains(x.levelId));
            //}

            //if (education_form != null)
            //{
            //    query = query.Where(x => x.educationForm == education_form);
            //}

            //if (education_language != null)
            //{
            //    query = query.Where(x => x.language == education_language);
            //}

            //if (program_name != null)
            //{
            //    query = query.Where(x => x.name.StartsWith(program_name));
            //}
            //int count = (int)Math.Ceiling((double)await query.CountAsync() / page_size);
            //if (page_current > count) { return new BadRequestObjectResult(new ErrorResponse(400, "Page_current не может быть больше количества страниц")); }

            //var programs = await query.Join(_context.Levels, program => program.levelId, level => level.id, (program, level) => new { EducationPrograms = program, Levels = level }).Join(_context.Faculties, programAndLevels => programAndLevels.EducationPrograms.facultyId, faculty => faculty.id, (program, faculty) => new EducationProgramResponse(program.EducationPrograms, faculty.name, program.Levels.name)).Skip((page_current - 1) * page_size).Take(page_size).ToListAsync();


            //return new OkObjectResult(new ProgramsResponse(programs, new Pagination(page_size, count, page_current)));
        }
    }
}
