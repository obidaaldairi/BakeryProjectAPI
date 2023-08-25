using Domin.Entity;
using FluentValidation;

namespace DataAccess.Validation
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.EnglishRoleName).NotEmpty().WithMessage("Please This Field Is Requird");
            RuleFor(x => x.ArabicRoleName).NotEmpty().WithMessage("Please This Field Is Requird");
        }
    }
}
