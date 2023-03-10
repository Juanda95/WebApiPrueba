using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Cuentas.Common.CreateCuentaCommand
{
    public class CreateCuentaCommandValidator : AbstractValidator<CreateCuentaCommand>
    {
        public CreateCuentaCommandValidator()
        {
            RuleFor(p => p.NumeroCuenta)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.TipoCuenta)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

                          
        }

    }
}
