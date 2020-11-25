using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MYDoctor.Infrastructure.Validation
{
   public class DoctorValidator:AbstractValidator<Doctor>
    {
        public DoctorValidator(IDoctorRepository doctorRepository,UserManager<ApplicationUser> userManager)
        {
            _doctorRepository = doctorRepository;
            _userManager = userManager;
            RuleFor(d=>d.Email).NotEmpty().EmailAddress().Must(UniqueEmail).WithMessage("This Email is already exist");
            RuleFor(d=>d.Name).NotEmpty().Must(UniqueName).WithMessage("This Name is already exist");
            RuleFor(d => d.Password).NotEmpty().MaximumLength(100).MinimumLength(6);
            RuleFor(d => d.ConfirmPassword).NotEmpty();
            RuleFor(d => d).Custom((d,context)=> {
                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var resuilt= passwordValidator.ValidateAsync(_userManager, null, d.Password).GetAwaiter().GetResult();
                if (!resuilt.Succeeded)
                {
                    resuilt.Errors.ToList().ForEach(
                        e=>context.AddFailure(nameof(d.Password),e.Description)
                        );
                    
                }
            });
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match Confirm Password");
                }
            });

        }

        private readonly IDoctorRepository _doctorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private bool UniqueName(string name)
        {
           
            if (!string.IsNullOrEmpty(name))
            {
                var category = _doctorRepository.GetAll(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (category == null) return true;
                return false;
            }

            return true;

        }
        private bool UniqueEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var category = _doctorRepository.GetAll(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
                if (category == null) return true;
                return false;
            }

            return true;

        }

    }
}
