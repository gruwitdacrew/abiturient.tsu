using EasyNetQ;
using Notification_Service.Models;
using System.Net.Mail;

namespace Notification_Service.Services.Notification_Component
{
    public class Notifications
    {
        private readonly IBus _rabbit;
        public Notifications()
        {
            _rabbit = RabbitHutch.CreateBus("host=localhost");
            _rabbit.PubSub.Subscribe<Notification>("my_subscription_id", async notification =>
            {
                await Send(notification);
            });
            Console.WriteLine("RabbitMQConsumer started. Waiting for messages...");
        }

        public async Task Send(Notification notification)
        {
            SmtpClient client = new SmtpClient("localhost", 1025); // Параметры подключения к SMTP-серверу MailDev
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage("Abiturient.tsu@example.com", notification.toEmail); // notification.toEmail
            message.Subject = notification.topic;
            message.Body = notification.message;

            try
            {
                client.Send(message);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }

    }
}
