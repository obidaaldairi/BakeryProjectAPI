using Domin.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please This Field Is Requird");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please This Field Is Requird");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Please This Field Is Requird");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please This Field Is Requird");
        }
    }
}
