using Faculty_Service.DBContext;
using Faculty_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Faculty_Service.Services.Faculty_Component
{

    public class Faculties
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        public Faculties(AppDbContext context)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://1c-mockup.kreosoft.space/");

            _context = context;
        }

        public async Task<IActionResult> GetPrograms(string[]? faculty_name,
                                                     string[]? education_level,
                                                     string? education_form,
                                                     string? education_language,
                                                     string? program_name,
                                                     int page_current,
                                                     int page_size)
        {
            if (page_current < 1 || page_size < 1) return new BadRequestObjectResult(new ErrorResponse(400, "Page_current и page_size должны быть положительными числами"));

            var query = _context.Programs.AsQueryable();

            if (faculty_name.Any() && faculty_name[0] != null)
            {
                List<string> faculties = await _context.Faculties.Where(x => faculty_name.Contains(x.name)).Select(x => x.id).ToListAsync();
                query = query.Where(x => faculties.Contains(x.facultyId));
            }

            if (education_level.Any() && education_level[0] != null)
            {
                List<string> levels = await _context.Levels.Where(x => education_level.Contains(x.name)).Select(x => x.id).ToListAsync();
                query = query.Where(x => levels.Contains(x.levelId));
            }

            if (education_form != null)
            {
                query = query.Where(x => x.educationForm == education_form);
            }

            if (education_language != null)
            {
                query = query.Where(x => x.language == education_language);
            }

            if (program_name != null)
            {
                query = query.Where(x => x.name.StartsWith(program_name));
            }
            int count = (int)Math.Ceiling((double)await query.CountAsync()/page_size);
            if (page_current > count) { return new BadRequestObjectResult(new ErrorResponse(400, "Page_current не может быть больше количества страниц")); }

            var programs = await query.Join(_context.Levels, program => program.levelId, level => level.id, (program, level) => new { EducationPrograms = program, Levels = level }).Join(_context.Faculties, programAndLevels => programAndLevels.EducationPrograms.facultyId, faculty => faculty.id, (program, faculty) => new EducationProgramResponse(program.EducationPrograms, faculty.name, program.Levels.name)).Skip((page_current-1) * page_size).Take(page_size).ToListAsync();


            return new OkObjectResult(new ProgramsResponse(programs, new Pagination(page_size, count, page_current)));
        }
         
        public async Task<IActionResult> Import()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/dictionary/education_levels");
            request.Headers.Add("Authorization", "Basic c3R1ZGVudDpueTZnUW55bjRlY2JCclA5bDFGeg==");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                List<Level> levels = JsonConvert.DeserializeObject<List<Level>>(await response.Content.ReadAsStringAsync());
                List<Level> levelsOld = await _context.Levels.ToListAsync();

                _context.Levels.RemoveRange(levelsOld.Except(levels, new LevelComparer()).ToList());
                _context.Levels.AddRange(levels.Except(levelsOld, new LevelComparer()).ToList());

            }
            else return new UnauthorizedObjectResult(string.Empty);



            request = new HttpRequestMessage(HttpMethod.Get, "api/dictionary/document_types");
            request.Headers.Add("Authorization", "Basic c3R1ZGVudDpueTZnUW55bjRlY2JCclA5bDFGeg==");

            response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                List<EducationDocumentTypeRaw> documentTypesRaw = JsonConvert.DeserializeObject<List<EducationDocumentTypeRaw>>(await response.Content.ReadAsStringAsync());
                List<EducationDocumentType> documentTypes = new List<EducationDocumentType>();
                List<NextLevel> nextLevels = new List<NextLevel>();

                foreach (var documentTypeRaw in documentTypesRaw)
                {
                    documentTypes.Add(new EducationDocumentType(documentTypeRaw));
                    foreach (var nextEducationLevel in documentTypeRaw.nextEducationLevels)
                    {
                        nextLevels.Add(new NextLevel(documentTypeRaw.educationLevel.id, nextEducationLevel.id));
                    }
                }

                List<EducationDocumentType> documentTypesOld = await _context.EducationDocumentTypes.ToListAsync();
                List<NextLevel> nextLevelsOld = await _context.NextLevels.ToListAsync();

                _context.EducationDocumentTypes.RemoveRange(documentTypesOld.Except(documentTypes, new EducationDocumentTypeComparer()));
                _context.EducationDocumentTypes.AddRange(documentTypes.Except(documentTypesOld, new EducationDocumentTypeComparer()));

                _context.NextLevels.RemoveRange(nextLevelsOld.Except(nextLevels, new NextLevelComparer()));
                _context.NextLevels.AddRange(nextLevels.Except(nextLevelsOld, new NextLevelComparer()));

            }
            else return new UnauthorizedObjectResult(string.Empty);



            request = new HttpRequestMessage(HttpMethod.Get, "api/dictionary/faculties");
            request.Headers.Add("Authorization", "Basic c3R1ZGVudDpueTZnUW55bjRlY2JCclA5bDFGeg==");

            response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                List<Faculty> faculties = JsonConvert.DeserializeObject<List<Faculty>>(await response.Content.ReadAsStringAsync());
                List<Faculty> facultiesOld = await _context.Faculties.ToListAsync();

                _context.Faculties.RemoveRange(facultiesOld.Except(faculties, new FacultyComparer()));
                _context.Faculties.AddRange(faculties.Except(facultiesOld, new FacultyComparer()));
            }
            else return new UnauthorizedObjectResult(string.Empty);



            request = new HttpRequestMessage(HttpMethod.Get, "api/dictionary/programs?page=1&size=100000");
            request.Headers.Add("Authorization", "Basic c3R1ZGVudDpueTZnUW55bjRlY2JCclA5bDFGeg==");

            response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                List<EducationProgramRaw> programsRaw = JsonConvert.DeserializeObject<EducationPrograms>(await response.Content.ReadAsStringAsync()).programs;
                List<EducationProgram> programs = new List<EducationProgram>();

                foreach (var programRaw in programsRaw)
                {
                    programs.Add(new EducationProgram(programRaw));
                }

                List<EducationProgram> programsOld = await _context.Programs.ToListAsync();

                _context.Programs.RemoveRange(programsOld.Except(programs, new EducationProgramComparer()));
                _context.Programs.AddRange(programs.Except(programsOld, new EducationProgramComparer()));
            }
            else return new UnauthorizedObjectResult(string.Empty);

            await _context.SaveChangesAsync();
            return new OkObjectResult(string.Empty);
        }
    }
}
