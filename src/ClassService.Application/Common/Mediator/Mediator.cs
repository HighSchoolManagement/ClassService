using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Common.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ClassService.Application.Common.Mediator
{
    public struct Unit
    {
        public static readonly Unit Value = new Unit();
    }
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return await handler.Handle((dynamic)request, cancellationToken);
        }
    }
}
