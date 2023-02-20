using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Clientes.Commands.CreateClienteCommand
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {

            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Genero)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Edad)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Identificacion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Direccion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Telefono)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Clienteid)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Contrasena)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Estado)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");



        }
    }
}
