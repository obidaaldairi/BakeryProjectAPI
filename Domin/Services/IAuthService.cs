using Domin.Entity;
using Domin.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Services
{
    public interface IAuthService
    {
        AuthResponse Login(LoginResponse request);
        string Register(User request);
    }
}
