
using SharedLib;

namespace iis_in
{
    public class Worker1 : BackgroundService
    {

        private ILogger<Worker1> _logger;

        public Worker1(ILogger<Worker1> logger)
        {
            _logger= logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("START Worker1");
            TempFileLog.Write("START Worker1");

            await Task.Delay(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"working1 {DateTime.UtcNow}");
                TempFileLog.Write("working1");
                await Task.Delay(1000);

            }

            TempFileLog.Write("exit_w1");
        }
    }
}
