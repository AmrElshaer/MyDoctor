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
            RuleFor(b => b.Category).NotEmpty().WithMessage(d=>validatorResource.GetResource("NotEmpty",nameof(d.Category)))
                .Must(UniqueName).WithMessage(d=>validatorResource.GetResource("IsExit",nameof(d.Category)));
            RuleFor(b=>b.Image).NotEmpty().WithMessage(d=>validatorResource.GetResource("NotEmpty",nameof(d.Image)));
            
        }

        private readonly ICategoryRepository _categoryRepository;

        private bool UniqueName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var category=  _categoryRepository.GetAll(x => x.Category.ToLower() == name.ToLower()).FirstOrDefault();
                if (category == null) return true;
                return false;
            }

            return true;
                
        }
    }
}
