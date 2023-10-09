using BusinessPortal.Data;
using BusinessPortal.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Validations
{
    public class PersonalValidation : AbstractValidator<Personal>
    {
        public PersonalValidation()
        {
            RuleFor(model => model.Id).NotEmpty();
            RuleFor(model => model.Email).EmailAddress().NotEmpty();
        }

        // Checks so its an excisting Id, will need to add as validation check
        public static async Task<bool> IdExcistsAsync(BusinessContext context, int id)
        {
            return await context.Personals.FirstOrDefaultAsync(x => x.Id == id) != null ? true : false;
        }
    }
}
