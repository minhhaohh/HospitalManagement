using FluentValidation;
using Hospital.Domain.DTO;

namespace Hospital.Domain.Validations
{
    public class PatientCreateValidator : AbstractValidator<PatientCreateDto>
    {
        public PatientCreateValidator()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Gender).NotNull();
        }
    }
}
