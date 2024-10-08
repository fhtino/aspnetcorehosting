
using SharedLib;

namespace iis_in
{
    public class Worker2 : BackgroundService
    {


        private ILogger<Worker2> _logger;

        public Worker2(ILogger<Worker2> logger)
        {
            _logger = logger;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("START Worker2");
            TempFileLog.Write("START Worker2");

            await Task.Delay(1);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"working2");
                TempFileLog.Write("working2");
                await Task.Delay(1000);
            }

            TempFileLog.Write("exit_w2");
        }
    }
}
