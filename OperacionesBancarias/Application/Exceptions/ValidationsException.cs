﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationsException : Exception
    {
        public ValidationsException() : base("Se han producido uno a mas errores de validación")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }

        public ValidationsException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
