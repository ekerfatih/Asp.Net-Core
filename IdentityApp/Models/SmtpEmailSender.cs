using System.Net;
using System.Net.Mail;

namespace IdentityApp.Models {
    public class SmtpEmailSender(string? host, int port, bool enableSSL, string? username, string? password) : IEmailSender {
        private string? _host = host;
        private int _port = port;
        private bool _enableSSL = enableSSL;
        private string? _username = username;
        private string? _password = password;

        public Task SendEmailAsyncs(string email, string subject, string body) {
            var client = new SmtpClient(_host, _port) {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = _enableSSL,
            };
            return client.SendMailAsync(new MailMessage(_username ?? "", email, subject, body) {
                IsBodyHtml = true,
            });
        }
    }
}