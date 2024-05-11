using EasyNetQ;
using EasyNetQ.Events;
using Microsoft.AspNetCore.Mvc;
using Users_Service.Controllers;
using Users_Service.Models;

namespace Users_Service.Services
{
    public class RabbitMQService
    {
        private readonly IBus _rabbit;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _rabbit = RabbitHutch.CreateBus("host=localhost");

            _rabbit.Rpc.RespondAsync<LoginRequest, IActionResult>(async request =>
            {
                return await Login(request);
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task<IActionResult> Login(LoginRequest request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<Users>();
                return await _context.LoginUser(request);
            }
        }
    }
}
