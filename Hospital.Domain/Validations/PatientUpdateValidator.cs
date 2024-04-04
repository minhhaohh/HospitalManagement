using FluentValidation;
using Hospital.Domain.DTO;

namespace Hospital.Domain.Validations
{
    public class PatientUpdateValidator : AbstractValidator<PatientUpdateDto>
    {
        public PatientUpdateValidator()
        {
            RuleFor(x => x.ChartNumber).NotNull();
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Gender).NotNull();
        }
    }
}
