using System;
using System.Collections.Generic;
using System.Text;
using ClassService.Application.Common.Mediator;

namespace ClassService.Application.Common.Mediator
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
