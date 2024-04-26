using Document_Service.DBContext;
using Document_Service.Models;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.EntityFrameworkCore;
using Users_Service.Models;

namespace Document_Service.Services
{
    public class RabbitMQService
    {
        private readonly IBus _rabbit;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _rabbit = RabbitHutch.CreateBus("host=localhost");

            _rabbit.Rpc.RespondAsync<Faculty_Service.Models.CompatibilityCheckRequest, bool>(request =>
            {
                return CheckCompatibility(request);
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task<bool> CheckCompatibility(Faculty_Service.Models.CompatibilityCheckRequest request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                EducationDocument educationDocument = await _context.Educations.Where(x => x.id == request.userId).Where(x => request.documentTypes.Contains(x.documentType)).FirstOrDefaultAsync();

                return educationDocument != null;
            }
        }
    }
}
