using FluentValidation;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Infrastructure.Validation
{
    public  class CategoryRelativieValidator:AbstractValidator<RelativeofBeatyandhealthy>
    {
        public CategoryRelativieValidator()
        {
            RuleFor(r => r.Address).NotEmpty();
            RuleFor(r => r.ImageOrVideo).NotEmpty();
            RuleFor(r => r.Subject).NotEmpty();
        }
    }
}
