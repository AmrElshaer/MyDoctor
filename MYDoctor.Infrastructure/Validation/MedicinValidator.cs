using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYDoctor.Infrastructure.Validation
{
    public  class MedicinValidator:AbstractValidator<Medicin>
    {
        public MedicinValidator()
        {
            RuleFor(m=>m.Name).NotEmpty();
            RuleFor(m => m.Price).NotEmpty();
            RuleFor(m=>m.Indications).NotEmpty();
            RuleFor(m => m).Custom((medicin,context)=> {
                if (!medicin.DiseaseMedicins.Any())
                {
                    context.AddFailure(nameof(DiseaseMedicin),"You Must Select Disease Related for this Medicin");
                }
            });
        }
    }
}
