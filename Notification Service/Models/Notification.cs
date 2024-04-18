namespace Notification_Service.Models
{
    public class Notification
    {
        public string topic { get; set; }
        public string toEmail { get; set; }
        public string message { get; set; }

        public Notification(string toEmail, string message, string topic)
        {
            this.topic = topic;
            this.toEmail = toEmail;
            this.message = message;
        }
    }
}
