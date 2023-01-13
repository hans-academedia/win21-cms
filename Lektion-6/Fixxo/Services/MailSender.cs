using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Fixxo.Services
{
	public class MailSender : IDisposable
	{
		private string _from;
		private string _smtp;
		private int _port;
		private string _username;
		private string _password;
        private bool disposed = false;

        public MailSender(string from, string smtp, int port, string username, string password)
		{
			_from = from;
			_smtp = smtp;
			_port = port;
			_username = username;
			_password = password;
		}

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                disposed = true;
        }





        public async Task SendAsync(string to, string subject, string body)
		{
			try
			{
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse(_from));
				email.To.Add(MailboxAddress.Parse(to));
				email.Subject = subject;
				email.Body = new TextPart(TextFormat.Html) { Text = body };

				using var smtpClient = new SmtpClient();
				await smtpClient.ConnectAsync(_smtp, _port, SecureSocketOptions.StartTls);
				await smtpClient.AuthenticateAsync(_username, _password);

				var result = await smtpClient.SendAsync(email);
				await smtpClient.DisconnectAsync(true);
			}
			catch { }
		}
	}
}
