using Application.Feauties.Clientes.Commands.DeleteClienteCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Cuentas.Common.DeleteCuentaCommand
{
    public class DeleteCuentaComandValidator : AbstractValidator<DeleteclienteComand>
    {
        public DeleteCuentaComandValidator()
        {
            RuleFor(p => p.Id)
                       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }

    }


}
