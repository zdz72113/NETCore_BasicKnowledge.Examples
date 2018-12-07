using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace GenericHostDemo.Jobs
{
    public class TestJob : IJob
    {
        private readonly ILogger _logger;

        public TestJob(ILogger<TestJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation(string.Format("[{0:yyyy-MM-dd hh:mm:ss}] TestJob is working.", DateTime.Now));
            return Task.CompletedTask;
        }
    }
}
