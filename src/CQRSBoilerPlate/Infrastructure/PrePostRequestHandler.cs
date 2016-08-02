using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSBoilerPlate.Infrastructure
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }

    public interface IPostRequestHandler<in TRequest, in TResponse>
    {
        void Handle(TRequest request, TResponse response);
    }
}
