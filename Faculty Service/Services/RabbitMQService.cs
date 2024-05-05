
using EasyNetQ;
using EasyNetQ.Topology;
using Faculty_Service.DBContext;
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

            _rabbit.Rpc.RespondAsync<string, List<string>>(request =>
            {
                return GetEducationDocumentTypes();
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task<List<string>> GetEducationDocumentTypes()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                return await _context.EducationDocumentTypes.Select(x => x.name).ToListAsync();
            }
        }
    }
}
