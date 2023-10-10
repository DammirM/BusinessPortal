using BusinessPortal.Models.DTO_s;
using FluentValidation;

namespace BusinessPortal.Validations
{
    public class CreateRequestValidation : AbstractValidator<RequestCreateDTO>
    {
        public CreateRequestValidation()
        {
            RuleFor(model => model.RequestTypeId).InclusiveBetween(1, 3);
            RuleFor(model => model.PersonalId).NotEqual(7);
        }
    }
}
