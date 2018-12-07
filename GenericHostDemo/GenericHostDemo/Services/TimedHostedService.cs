using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostDemo.Services
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly HostDemoContext _context;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, HostDemoContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            int id = 1;
            var product = _context.Products.Find(id);
            product.Count++;
            _context.SaveChanges();

            _logger.LogInformation($"Processed {product.Name} at {DateTime.Now:yyyy-MM-dd hh:mm:ss}, current count is {product.Count}.");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            base.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
