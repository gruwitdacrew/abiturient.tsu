
using Application_Service.DBContext;
using Application_Service.Models;
using Application_Service.Models.Requests;
using Application_Service.Services.Abiturient_Component;
using EasyNetQ;
using Faculty_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Users_Service.Models;
using Users_Service.Services;

namespace Application_Service.Services
{
    public class RabbitMQService
    {
        private readonly IBus _rabbit;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _rabbit = RabbitHutch.CreateBus("host=localhost");

            _rabbit.PubSub.Subscribe<User>("user_subscription", async user =>
            {
                await CreateOrEditAbiturient(user);
            });
            _rabbit.PubSub.Subscribe<List<EducationProgramPlain>>("educationProgram_subscription", async list =>
            {
                await UpdateEducationProgramList(list);
            });
            _rabbit.Rpc.RespondAsync<ApplicationsRequest, IActionResult>(async request =>
            {
                return await GetApplications(request);
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task CreateOrEditAbiturient(User user)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                Abiturient abiturient = await _context.Abiturients.Where(x => x.id == user.Id.ToString()).FirstOrDefaultAsync();
                if (abiturient == null)
                {
                    _context.Abiturients.Add(new Abiturient(user, ""));
                    _context.Applications.Add(new Application(user.Id.ToString()));
                }
                else
                {
                    _context.Abiturients.Remove(abiturient);

                    abiturient = new Abiturient(user, "");
                    _context.Abiturients.Add(abiturient);
                }
                _context.SaveChangesAsync();
            }
        }
        public async Task UpdateEducationProgramList(List<EducationProgramPlain> list)
        {
            List<Models.EducationProgram> programs = list.Select(educationProgramPlain => new Models.EducationProgram(educationProgramPlain)).ToList();
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var programsOld = await _context.Programs.ToListAsync();

                _context.Programs.RemoveRange(programsOld.Except(programs, new Models.EducationProgram.EducationProgramComparer()));
                _context.Programs.AddRange(programs.Except(programsOld, new Models.EducationProgram.EducationProgramComparer()));

                await _context.SaveChangesAsync();
            }
        }
        public async Task<IActionResult> GetApplications(ApplicationsRequest applicationsRequest)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<Managers>();
                return await _context.GetApplications(applicationsRequest);
            }
        }
    }
}
