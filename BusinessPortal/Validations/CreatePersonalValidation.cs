using BusinessPortal.DTO_s;
using BusinessPortal.Models;
using FluentValidation;

namespace BusinessPortal.Validations
{
    public class CreatePersonalValidation : AbstractValidator<PersonalCreateDTO>
    {
        public CreatePersonalValidation()
        {
            RuleFor(model => model.FullName).NotEmpty().MinimumLength(2).MaximumLength(35);
            RuleFor(model => model.Email).EmailAddress().NotEmpty();
        }
    }
}
