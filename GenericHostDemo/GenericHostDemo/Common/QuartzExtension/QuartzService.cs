using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostDemo.Common.QuartzExtension
{
    public class QuartzService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;

        public QuartzService(ILogger<QuartzService> logger, IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start quartz scheduler...");
            await _scheduler.Start(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop quartz scheduler...");
            await _scheduler.Shutdown(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}
