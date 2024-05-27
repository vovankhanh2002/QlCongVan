using BusinessLayer.Repository.IRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class EmailSenderCV : Ikhanh
    {

        public EmailSenderCV()
        {

        }
        public async Task SendEmailCV(List<string> toAddress, string subject, string htmlMessage, string attachmentFilePath = null)
        {
            // Set up the message
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Văn Khánh", "khanhvo06062002@gmail.com"));
            foreach (var address in toAddress)
            {
                emailMessage.To.Add(new MailboxAddress(address, address));

            }
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = "<h1>Hello World</h1><p>This is a test email with HTML content.</p>",
                TextBody = htmlMessage
            };
            emailMessage.Subject = subject;
            var multipart = new Multipart("mixed");

            // Thêm phần đính kèm vào multipart
            if (!string.IsNullOrEmpty(attachmentFilePath) && File.Exists(attachmentFilePath))
            {
                var attachment = new MimePart()
                {
                    Content = new MimeContent(File.OpenRead(attachmentFilePath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(attachmentFilePath)
                };

                multipart.Add(attachment);
            }

            // Tạo phần nội dung email và thêm vào multipart
            bodyBuilder.HtmlBody = htmlMessage;
            multipart.Add(bodyBuilder.ToMessageBody());

            emailMessage.Body = multipart;
            // Send email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("khanhvo06062002@gmail.com", "ibppgegiozcymhsq");

                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
