using System.Net;
using System.Net.Mail;
using System.Transactions;

namespace EmailSender;

public class EmailService
{
    private readonly SmtpConfig _smtpConfig;

    public EmailService(SmtpConfig smtpConfig)
    {
        _smtpConfig = smtpConfig;
    }

    public void Send(string subject, string body, bool isBodyHtml, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
    {
        if (Transaction.Current != null)
        {
            Transaction.Current.TransactionCompleted += (_, args) =>
            {
                if (args.Transaction?.TransactionInformation.Status == TransactionStatus.Committed)
                {
                    SendInternal(subject, body, isBodyHtml, to, cc, bcc);
                }
            };
        }
        else
        {
            SendInternal(subject, body, isBodyHtml, to, cc, bcc);
        }
    }

    private void SendInternal(string subject, string body, bool isBodyHtml, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpConfig.From),
            Subject = subject,
            IsBodyHtml = isBodyHtml,
            Body = body,
        };

        var hasAtLeastOneToAddress = false;

        if (to is not null)
        {
            foreach (var toAddress in to)
            {
                if (!string.IsNullOrWhiteSpace(toAddress))
                {
                    hasAtLeastOneToAddress = true;
                    mailMessage.To.Add(new MailAddress(toAddress));
                }
            }
        }

        if (!hasAtLeastOneToAddress)
        {
            throw new ArgumentException("There is no 'to' address to send an email!");
        }

        if (cc is not null)
        {
            foreach (var ccAddress in cc)
            {
                if (!string.IsNullOrWhiteSpace(ccAddress))
                {
                    mailMessage.CC.Add(new MailAddress(ccAddress));
                }
            }
        }

        if (bcc is not null)
        {
            foreach (var bccAddress in bcc)
            {
                if (!string.IsNullOrWhiteSpace(bccAddress))
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }
        }

        var smtp = new SmtpClient
        {
            Host = _smtpConfig.Host,
            Port = _smtpConfig.Port,
            EnableSsl = true,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        smtp.Credentials = new NetworkCredential(_smtpConfig.From, _smtpConfig.Password);

        smtp.Send(mailMessage);
    }
}