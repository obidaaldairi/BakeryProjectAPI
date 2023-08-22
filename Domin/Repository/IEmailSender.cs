using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Domin.Repository
{
    public interface IEmailSender
    {
        //Task SendEmailAsync(string email, string subject, string htmlMessage);
        //void sendMail(string To,
        //     System.Net.Mail.Attachment attachment,
        //     string From,
        //     string password,
        //     string From_Alias,
        //     string subject,
        //     string body,
        //     int Port,
        //     string Host,
        //     bool EnableSsl,
        //     string FromCC = "");
        Task SendEmailAsync(string email, string subject, string body);
    }
}
