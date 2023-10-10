using BusinessPortal.Data;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Validations
{
    public class PersonalUpdateValidation : AbstractValidator<PersonalUpdateDTO>
    {
        public PersonalUpdateValidation()
        {
            RuleFor(model => model.FullName).NotEmpty().MinimumLength(2).MaximumLength(35);
            RuleFor(model => model.Email).EmailAddress().NotEmpty();
        }
    }
}
