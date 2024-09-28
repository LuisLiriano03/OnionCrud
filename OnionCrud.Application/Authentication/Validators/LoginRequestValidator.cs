using FluentValidation;
using OnionCrud.Application.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .WithMessage("Email is required");

            RuleFor(request => request.PasswordHash)
                .NotEmpty()
                .WithMessage("Password is required");

        }

    }
}
