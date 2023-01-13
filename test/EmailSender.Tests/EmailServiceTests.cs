using Xunit;

namespace EmailSender.Tests
{
    public class EmailServiceTests
    {
        [Fact]
        public void Send_ShouldSendTheEmail_WhenParametersAreValid()
        {
            var sender = new EmailService(GetSmtpConfig());
            sender.Send(
                subject: "test", 
                body: "<h1>test</h1><br>hello", 
                isBodyHtml: true, 
                to: new[] { "test@example.com" }, 
                cc: new[] { "test2@example.com" },
                bcc: new[] { "test3@example.com" });
        }


        private static SmtpConfig GetSmtpConfig() =>
            new()
            {
                Host = "smtp.ethereal.email",
                Port = 587,
                From = "blaise36@ethereal.email",
                Password = "fFyfjdTemTgsTF2J8e"
            };
    }
}