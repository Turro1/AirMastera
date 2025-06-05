namespace AirMastera.Infrastructure.Api.BackgroundServices
{
    public class QuoteBackgroundService : BackgroundService
    {
        private readonly ILogger<QuoteBackgroundService> _logger;

        private readonly List<string> _quotes = new()
        {
            "Keep it simple.",
            "Talk is cheap. Show me the code.",
            "Code never lies, comments sometimes do.",
            "Make it work, make it right, make it fast."
        };

        public QuoteBackgroundService(ILogger<QuoteBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("QuoteBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var random = new Random();
                var quote = _quotes[random.Next(_quotes.Count)];

                _logger.LogInformation("Quote of the moment: {Quote}", quote);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("QuoteBackgroundService is stopping.");
        }
    }
}