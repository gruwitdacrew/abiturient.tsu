
using Application_Service.DBContext;
using Application_Service.Models;
using EasyNetQ;
using Faculty_Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Users_Service.Models;

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
                    _context.Abiturients.AddAsync(new Abiturient(user, ""));
                }
                else
                {
                    _context.Abiturients.Remove(abiturient);

                    abiturient = new Abiturient(user, "");
                    await _context.Abiturients.AddAsync(abiturient);
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
    }
}
