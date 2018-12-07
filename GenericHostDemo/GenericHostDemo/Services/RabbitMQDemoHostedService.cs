using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TestRabbitMQ.Message;

namespace GenericHostDemo.Services
{
    public class RabbitMQDemoHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IBus _bus;

        public RabbitMQDemoHostedService(ILogger<RabbitMQDemoHostedService> logger, IBus bus)
        {
            this._logger = logger;
            this._bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.Subscribe<DemoMessage>("demo_subscription_1", HandleDemoMessage);
            return Task.CompletedTask;
        }

        private void HandleDemoMessage(DemoMessage demoMessage)
        {
            _logger.LogInformation($"Got Message : {demoMessage.Id} {demoMessage.Text} {demoMessage.CreatedTime}");
        }
    }
}
