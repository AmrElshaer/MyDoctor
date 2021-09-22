using FluentValidation;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System.Linq;

namespace MYDoctor.Infrastructure.Validation
{
    public class CategoryValidator : AbstractValidator<BeatyandHealthy>
    {
        public CategoryValidator(ICategoryRepository categoryRepository,IValidatorResource validatorResource)
        {
            _categoryRepository = categoryRepository;
            RuleFor(b => b.Category).NotNull().NotEmpty().
               WithMessage(d => validatorResource.GetResource("NotEmpty", nameof(d.Category)));
             RuleFor(b=>b).Must(UniqueName).WithMessage(d=>validatorResource.GetResource("IsExit",nameof(d.Category)));
            RuleFor(b=>b.Image).NotEmpty().WithMessage(d=>validatorResource.GetResource("NotEmpty",nameof(d.Image)));
            
        }

        private readonly ICategoryRepository _categoryRepository;

        private bool UniqueName(BeatyandHealthy category)
        {
            if (category.Category==null)
            {
                return false;
            }

            return _categoryRepository.GetAll(x => 
            x.Category.ToLower()== category.Category.ToLower()&&(category.Id==0||x.Id!=category.Id))
                .FirstOrDefault() == null;
        }
    }
}
