using Hangfire;
using MediatR;
using Newtonsoft.Json;

namespace VendorBoilerplate.Infrastructure.Messaging.Hangfire
{
    public static class MediatorExtensions
    {
        public static void Enqueue<TRequest>(this IMediator mediator, string jobName, TRequest request) where TRequest : IRequest
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(jobName, request));
        }

        public static void Enqueue<TRequest>(this IMediator mediator, TRequest request) where TRequest : IRequest
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Send(request));
        }

        public static void EnqueuePublish<TNotification>(this IMediator mediator, TNotification notification) where TNotification : INotification
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangfireBridge>(bridge => bridge.Publish(notification));
        }

    }

    public static class HangfireConfigurationExtensions
    {
        public static void UseMediatR(this IGlobalConfiguration configuration)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            configuration.UseSerializerSettings(jsonSettings);
        }
    }
}
