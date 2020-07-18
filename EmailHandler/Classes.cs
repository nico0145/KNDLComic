using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
namespace EmailHandler
{
    public class Email
    {
        public string To { set; get; }
        public string From { get { return "em@il.com"; } }
        public MemoryStream Attachment { set; get; }
        public string AttachmentFileName { set; get; }
        public Email()
        {
            Attachment = new MemoryStream();
        }
        public void SendEmail()
        {
            //create the mail message
            MailMessage mail = new MailMessage();

            //set the addresses
            mail.From = new MailAddress(From);
            mail.To.Add(To);

            //set the content
            mail.Subject = "Sent with KNDLComic";
            mail.Body = "";

            //create the attachment from a stream. Be sure to name the data with a file and 
            //media type that is respective of the data
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(Attachment, ct);
            attach.ContentDisposition.FileName = AttachmentFileName + ".pdf";
            mail.Attachments.Add(attach);

            //send the message
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com", 587); //hotmail smtp, adapt to yours
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(From, "PasswordHere");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            Attachment.Close();
        }
    }
}
