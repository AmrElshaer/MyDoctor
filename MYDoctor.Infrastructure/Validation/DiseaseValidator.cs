using FluentValidation;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Infrastructure.Validation
{
    public  class DiseaseValidator: AbstractValidator<Disease>
    {
        public DiseaseValidator()
        {
            RuleFor(d => d.DiseaseName).NotEmpty();
            RuleFor(d => d.Subject).NotEmpty();
            RuleFor(d => d.Reasons).NotEmpty();
            RuleFor(d => d.Protection).NotEmpty();
            RuleFor(d => d.Image).NotEmpty();

        }
    }
}
