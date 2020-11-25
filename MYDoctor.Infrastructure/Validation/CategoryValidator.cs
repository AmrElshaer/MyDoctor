using FluentValidation;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Linq;

namespace MYDoctor.Infrastructure.Validation
{
    public class CategoryValidator : AbstractValidator<BeatyandHealthy>
    {
        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(b => b.Category).NotEmpty().Must(UniqueName).WithMessage("This Category is already exist");
            RuleFor(b=>b.Image).NotEmpty();
            
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
