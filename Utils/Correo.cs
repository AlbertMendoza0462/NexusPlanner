
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NexusPlanner.Utils
{
    public class Correo
    {
        private MailAddress SenderAddress { get; set; }
        private MailAddress ReciberAddress { get; set; }
        private SmtpClient Smtp { get; set; }
        private MailMessage Message { get; set; }

        public Correo(string reciberAddress, string reciberName, string subject, string body)
        {
            SenderAddress = new MailAddress(System.Environment.GetEnvironmentVariable("EMAIL_ADDRESS") ?? "", System.Environment.GetEnvironmentVariable("EMAIL_NAME"), Encoding.UTF8);
            ReciberAddress = new MailAddress(reciberAddress, reciberName, Encoding.UTF8);

            Message = new MailMessage();
            Message.Subject = subject;
            Message.SubjectEncoding = Encoding.UTF8;
            Message.Body = body;
            Message.Sender = SenderAddress;
            Message.From = SenderAddress;
            Message.To.Add(ReciberAddress);
            Message.IsBodyHtml = true;
            Message.BodyEncoding = Encoding.UTF8;

            Smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SenderAddress.Address, System.Environment.GetEnvironmentVariable("EMAIL_PASSWORD"))
            };
        }

        public void Send()
        {
            if (SenderAddress is null) throw new Exception("SenderAddress is empty");
            else if (ReciberAddress is null) throw new Exception("ReciberAddress is empty");
            else if (Message is null) throw new Exception("Message is empty");
            else Smtp.Send(Message);
        }
    }
}
