using Application_Service.DBContext;
using Application_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application_Service.Models.Requests;

namespace Application_Service.Services.Abiturient_Component
{
    public class Managers
    {
        private readonly AppDbContext _context;
        public Managers(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetApplications(ApplicationsRequest applicationRequest)
        {
            if (applicationRequest.page_current < 1 || applicationRequest.page_size < 1) return new ObjectResult(new ErrorResponse(400, "Page_current и page_size должны быть положительными числами"));

            var query = _context.Applications.AsQueryable();

            if (applicationRequest.faculty_names != null && applicationRequest.faculty_names.Any() && applicationRequest.faculty_names[0] != null)
            {
                //var applicationIds = await _context.ApplicationPrograms.Where(x => faculty_names.ToList().Contains(x.educationProgram.name)).Select(x => x.id).ToListAsync();

                query = query.Where(x => _context.ApplicationPrograms.Where(x => applicationRequest.faculty_names.ToList().Contains(x.educationProgram.name)).Select(x => x.id).ToList().Contains(x.id));
            }

            if (applicationRequest.fullName != null)
            {
                query = query.Where(x => x.abiturient.fullName.Contains(applicationRequest.fullName));
            }

            if (applicationRequest.status != null)
            {
                query = query.Where(x => x.status == applicationRequest.status);
            }

            query = query.Where(x => (x.abiturient.managerId != null) == applicationRequest.hasManager);

            query = query.Where(x => (x.abiturient.managerId == applicationRequest.managerId) == applicationRequest.myAbiturients);

            if (applicationRequest.lastModifiedAsc) query = query.OrderBy(x => x.lastModified);
            else query = query.OrderByDescending(x => x.lastModified);

            if (applicationRequest.program_name != null)
            {
                //admissions.Where(admission => admission.Programs.Any(program => program.Name == targetProgramName)).ToList();

                query = query.Where(x => x.applicationPrograms.Any(program => program.educationProgram.name == applicationRequest.program_name));
            }

            int count = (int)Math.Ceiling((double)await query.CountAsync() / applicationRequest.page_size);
            if (applicationRequest.page_current > count) { return new ObjectResult(new ErrorResponse(400, "Page_current не может быть больше количества страниц")); }

            var applications = await query.Skip((applicationRequest.page_current - 1) * applicationRequest.page_size).Take(applicationRequest.page_size).ToListAsync();


            return new OkObjectResult(new ApplicationsResponse(applications.Select(t => new ApplicationResponse(t)).ToList(), new Pagination(applicationRequest.page_size, count, applicationRequest.page_current)));
        }
    }
}
