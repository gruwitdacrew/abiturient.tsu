using EasyNetQ;
using Faculty_Service.DBContext;
using Faculty_Service.Models;
using Microsoft.EntityFrameworkCore;
using Users_Service.Models;

namespace Faculty_Service.Services
{
    public class RabbitMQService
    {
        private readonly IBus _rabbit;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _rabbit = RabbitHutch.CreateBus("host=localhost");
            _rabbit.PubSub.Subscribe<string>("token_unsubscription_faculty", async userId =>
            {
                await DeleteAccessToken(userId);
            });
            _rabbit.PubSub.Subscribe<TokenMessage>("token_subscription_faculty", async tokenMessage =>
            {
                await SaveAccessToken(tokenMessage);
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task SaveAccessToken(TokenMessage tokenMessage)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (await _context.Abiturients.Where(p => p.id == tokenMessage.userId).FirstOrDefaultAsync()!= null)
                {
                    await _context.Abiturients.Where(p => p.id == tokenMessage.userId).ExecuteUpdateAsync(p => p.SetProperty(t => t.accessToken, tokenMessage.token));
                }
                else
                {
                    Abiturient abiturient = new Abiturient(tokenMessage.userId, tokenMessage.token);
                    await _context.Abiturients.AddAsync(abiturient);
                }
                await _context.SaveChangesAsync();

                Console.WriteLine("Token Saved");
            }
        }

        public async Task DeleteAccessToken(string userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await _context.Abiturients.Where(p => p.id == userId).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();

                Console.WriteLine("Token Deleted");
            }
        }
    }
}
