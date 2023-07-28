using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace VendorBoilerplate.Application.Infrastructures
{
    public class RequestLogger<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("VendorBoilerplate Request: {Name} {@Request}", name, request);
            return Task.CompletedTask;
        }
    }
}
