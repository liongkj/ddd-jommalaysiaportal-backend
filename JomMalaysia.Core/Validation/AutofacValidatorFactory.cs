using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FluentValidation;

namespace JomMalaysia.Core.Validation
{
    public class AutofacValidatorFactory:ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (_context.TryResolve(validatorType, out instance))
            {
                var validator = instance as IValidator;
                return validator;
            }

            return null;
        }
    }
}
