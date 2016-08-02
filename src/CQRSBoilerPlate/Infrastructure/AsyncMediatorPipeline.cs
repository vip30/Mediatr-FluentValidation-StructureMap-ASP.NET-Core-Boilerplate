using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSBoilerPlate.Infrastructure
{
    public class AsyncMediatorPipeline<TRequest, TResponse>
        : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {

        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;
        private readonly IPostRequestHandler<TRequest, TResponse>[] _postRequestHandlers;

        public AsyncMediatorPipeline(
            IAsyncRequestHandler<TRequest, TResponse> inner,
            IPreRequestHandler<TRequest>[] preRequestHandlers,
            IPostRequestHandler<TRequest, TResponse>[] postRequestHandlers
            )
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
            _postRequestHandlers = postRequestHandlers;
        }

        public async Task<TResponse> Handle(TRequest message)
        {

            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(message);
            }

            var result = await _inner.Handle(message);

            foreach (var postRequestHandler in _postRequestHandlers)
            {
                postRequestHandler.Handle(message, result);
            }

            return result;
        }
    }
}
