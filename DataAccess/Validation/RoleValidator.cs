using Domin.Entity;
using FluentValidation;

namespace DataAccess.Validation
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Please This Field Is Requird");
        }
    }
}
