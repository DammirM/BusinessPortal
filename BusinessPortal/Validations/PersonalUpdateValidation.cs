using BusinessPortal.Data;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Validations
{
    public class PersonalUpdateValidation : AbstractValidator<PersonalUpdateDTO>
    {
        private readonly BusinessContext _context;

        public PersonalUpdateValidation(BusinessContext context)
        {
            this._context = context;

            RuleFor(model => model.Id).NotEmpty().MustAsync(async (Id, CancellationToken) => await IdExcists(Id, CancellationToken))
        .WithMessage("Invalid Personal Id!");
            RuleFor(model => model.FullName).NotEmpty().MinimumLength(2).MaximumLength(35);
            RuleFor(model => model.Email).EmailAddress().NotEmpty();
        }

        public async Task<bool> IdExcists(int id, CancellationToken cToken)
        {
            Console.WriteLine("Hello from IdExcists");
            return await _context.Personals.AnyAsync(x => x.Id == id, cToken);
        }
    }
}
