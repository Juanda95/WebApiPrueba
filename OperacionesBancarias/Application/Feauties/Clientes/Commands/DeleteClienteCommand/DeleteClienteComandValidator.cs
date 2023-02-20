using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Clientes.Commands.DeleteClienteCommand
{
    internal class DeleteClienteComandValidator : AbstractValidator<DeleteclienteComand>
    {
        public DeleteClienteComandValidator()
        {
            RuleFor(p => p.Id)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
