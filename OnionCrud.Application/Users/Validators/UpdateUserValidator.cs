using FluentValidation;
using OnionCrud.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("The full name is required")
                .MaximumLength(50).WithMessage("The full name cannot exceed 50 characters");


            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("The email is required")
                .MaximumLength(100).WithMessage("The email cannot exceed 100 characters");

            RuleFor(user => user.PasswordHash)
                .NotEmpty().WithMessage("The password is required")
                .MaximumLength(100).WithMessage("The password cannot exceed 100 characters");

        }

    }

}
