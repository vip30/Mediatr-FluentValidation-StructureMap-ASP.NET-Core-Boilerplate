using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSBoilerPlate.Infrastructure
{
    public class ValidatorHandler<TRequest, TResponse>
     : IRequestHandler<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {

        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorHandler(IRequestHandler<TRequest, TResponse> inner,
            IValidator<TRequest>[] validators)
        {
            _inner = inner;
            _validators = validators;
        }

        public TResponse Handle(TRequest message)
        {
            var context = new ValidationContext(message);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return _inner.Handle(message);
        }
    }
}
