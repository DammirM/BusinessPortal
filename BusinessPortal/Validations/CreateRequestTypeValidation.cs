using BusinessPortal.Data;
using BusinessPortal.Models;
using BusinessPortal.Models.DTO_s;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal.Validations
{
    public class CreateRequestTypeValidation : AbstractValidator<RequestTypeCreateDTO>
    {
        public CreateRequestTypeValidation()
        {
            // Going to add a integer range for these, so max is maybe 50 days or something
            // Remove when done
            RuleFor(model => model.Vabb).NotEmpty();
            RuleFor(model => model.Sick).NotEmpty();
            RuleFor(model => model.Vacation).NotEmpty();
        }
    }
}
