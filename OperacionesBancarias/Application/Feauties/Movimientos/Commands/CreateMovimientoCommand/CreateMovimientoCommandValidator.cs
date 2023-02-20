using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Movimientos.Commands.CreateMovimientoCommand
{
    internal class CreateMovimientoCommandValidator : AbstractValidator<CreateMovimientoCommand>
    {
        public CreateMovimientoCommandValidator()
        {
            

        }

    }
}
