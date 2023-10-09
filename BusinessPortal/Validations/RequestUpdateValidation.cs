using BusinessPortal.Models.DTO_s;
using FluentValidation;

namespace BusinessPortal.Validations
{
    public class RequestUpdateValidation : AbstractValidator<RequestUpdateDTO>
    {
        public RequestUpdateValidation()
        {
            RuleFor(model => model.RequestTypeId).InclusiveBetween(1, 3);
            RuleFor(model => model.PersonalId).NotEqual(7);
        }
    }
}
