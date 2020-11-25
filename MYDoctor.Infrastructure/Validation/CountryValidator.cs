using FluentValidation;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System.Linq;
namespace MYDoctor.Infrastructure.Validation
{
    public class CountryValidator: AbstractValidator<Country>
    {
        public CountryValidator(ICountryRepository countryRepository)
        {
            RuleFor(c => c.Name).NotEmpty().Must(UniqueName).WithMessage("This City is already exist"); ;
            _countryRepository = countryRepository;
        }

        private readonly ICountryRepository _countryRepository;
        private bool UniqueName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var category = _countryRepository.GetAll(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (category == null) return true;
                return false;
            }

            return true;

        }
    }
}
