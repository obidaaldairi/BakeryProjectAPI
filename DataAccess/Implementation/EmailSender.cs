using DataAccess.Context;
using Domin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task SendEmail(string email)
        {
            var check = IsEmailExist(email);
            if(check) { 
            
             await Task.CompletedTask;
            }

        }


        private bool IsEmailExist(string email)
        {
            var user = _context.tblUsers.Where(q => q.IsDeleted == false && q.Email == email).FirstOrDefault();
            if(user is  null) { return false; }
            return true;
        }
    }
}
