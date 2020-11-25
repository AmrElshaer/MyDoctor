using FluentValidation;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System.Linq;
namespace MYDoctor.Infrastructure.Validation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator(ICityRepository cityRepository)
        {
            RuleFor(c => c.Name).NotEmpty().Must(UniqueName).WithMessage("This City is already exist"); ;
            _cityRepository = cityRepository;
        }

        private readonly ICityRepository _cityRepository;
        private bool UniqueName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var category = _cityRepository.GetAll(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (category == null) return true;
                return false;
            }

            return true;

        }
    }
}
