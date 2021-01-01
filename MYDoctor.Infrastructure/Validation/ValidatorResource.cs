using Microsoft.Extensions.Localization;
using MYDoctor.Core.Application.IHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Validation
{
    public class ValidatorResource : IValidatorResource
    {
        private readonly IStringLocalizer<ValidatorResource> _validatorResource;

        public ValidatorResource(IStringLocalizer<ValidatorResource> validatorResoucre)
        {
            _validatorResource = validatorResoucre;  
        }
        public string GetResource(string field,string message)
        {
            return $"{_validatorResource[message]} {_validatorResource[field]}";
        }
    }
}
