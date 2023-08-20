using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Domin.Repository
{
    public interface IEmailSender
    {
        Task SendEmail(string email);
    }
}
