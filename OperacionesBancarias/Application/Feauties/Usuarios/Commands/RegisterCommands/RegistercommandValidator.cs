using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Usuarios.Commands.RegisterCommands
{
    public class RegistercommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegistercommandValidator()
        {
            RuleFor(p => p.Nombre)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Apellido)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Email)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
                   .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.UserName)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Password)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.confirmPassword)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
                   .Equal(P => P.Password).WithMessage("{PropertyName} debe ser igual a Password");
        }

    }
}
