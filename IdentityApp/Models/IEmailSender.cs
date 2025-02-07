namespace IdentityApp.Models {
    public interface IEmailSender {
        Task SendEmailAsyncs(string email, string subject, string body);
    }
}