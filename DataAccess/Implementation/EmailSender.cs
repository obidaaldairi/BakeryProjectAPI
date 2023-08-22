using DataAccess.Context;
using Domin.Repository;
using FluentEmail.Core.Models;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly AppDbContext _context;

        public EmailSender(AppDbContext context)
        {
            _context = context;
        }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            var from = _context.tblWebConfigurations.Where(x => x.ConfigKey == "From").FirstOrDefault().ConfigValue;
            var password = _context.tblWebConfigurations.Where(x => x.ConfigKey == "Password").FirstOrDefault().ConfigValue;
            var Port = Convert.ToInt32(_context.tblWebConfigurations.Where(x => x.ConfigKey == "Port").FirstOrDefault().ConfigValue);
            var SMTP = _context.tblWebConfigurations.Where(x => x.ConfigKey == "SMTP").FirstOrDefault().ConfigValue;


            var client = new SmtpClient(SMTP, Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from,
                "tyagwdnmxgrkkfky")
            };

            return client.SendMailAsync(
                new MailMessage(from,
                                to: email,
                                subject,
                                body
                                ));
        }
    }
}
