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

namespace Application.Feauties.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteCommandValidator()
        {

            RuleFor(p => p.Nombre)
                    .MaximumLength(50).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Genero)

                    .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");       

            RuleFor(p => p.Estado)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");



        }
    }

}
