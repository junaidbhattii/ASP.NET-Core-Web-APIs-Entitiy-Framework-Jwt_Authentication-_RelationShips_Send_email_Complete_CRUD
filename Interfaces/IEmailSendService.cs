namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IEmailSendService
    {
        public Task SendEmailToUser(string email, string subject, string message);
    }
}
