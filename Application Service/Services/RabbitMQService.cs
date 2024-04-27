
using Application_Service.DBContext;
using Application_Service.Models;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
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
    }
}
