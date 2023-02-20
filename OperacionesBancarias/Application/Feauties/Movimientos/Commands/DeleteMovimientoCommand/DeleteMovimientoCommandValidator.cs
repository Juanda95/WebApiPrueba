using Application.Feauties.Clientes.Commands.DeleteClienteCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Movimientos.Commands.DeleteMovimientoCommand
{
    public class DeleteMovimientoCommandValidator : AbstractValidator<DeleteclienteComand>
    {
        public DeleteMovimientoCommandValidator()
        { 
            RuleFor(p => p.Id)
                       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
