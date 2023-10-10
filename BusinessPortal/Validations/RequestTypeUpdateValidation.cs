using BusinessPortal.Data;
using BusinessPortal.Models.DTO_s;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Validations
{
    public class RequestTypeUpdateValidation : AbstractValidator<RequestTypeUpdateDTO>
    {
        private readonly BusinessContext _context;

        public RequestTypeUpdateValidation(BusinessContext context)
        {
            this._context = context;

            // Going to add a integer range for these, so max is maybe 50 days or something
            // Remove when done (have to be same as create validation)
            RuleFor(model => model.Id).NotEmpty().MustAsync(async (Id, CancellationToken) => await IdExcists(Id, CancellationToken))
        .WithMessage("Invalid Personal Id!");
            RuleFor(model => model.Vabb).NotEmpty();
            RuleFor(model => model.Sick).NotEmpty();
            RuleFor(model => model.Vacation).NotEmpty();
        }

        public async Task<bool> IdExcists(int id, CancellationToken cToken)
        {
            return await _context.RequestTypes.AnyAsync(x => x.Id == id);
        }
    }
}
