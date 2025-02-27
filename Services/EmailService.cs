using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace WebApplication4.Services
{
	public class EmailService
	{
		public void SendEmail(string to, string subject, string body)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("fatimafa@code.edu.az"));
			email.To.Add(MailboxAddress.Parse(to));
			email.Subject = subject;
			email.Body = new TextPart(TextFormat.Html) { Text = body };

			using var smtp = new SmtpClient();
			smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
			smtp.Authenticate("fatimafa@code.edu.az", "ltba jxgr rwpj elmb");
			smtp.Send(email);
			smtp.Disconnect(true);

		}
	}
}
