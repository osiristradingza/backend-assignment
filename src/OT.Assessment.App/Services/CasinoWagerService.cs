using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Models.DTOs;

namespace OT.Assessment.App.Services
{
    public class CasinoWagerService
    {
        private readonly CasinoContext _dbContext;
        private readonly ILogger<CasinoWagerService> _logger;
        private readonly RabbitMQService _rabbitMqService; 

        public CasinoWagerService(CasinoContext dbContext, ILogger<CasinoWagerService> logger, RabbitMQService rabbitMqService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _rabbitMqService = rabbitMqService; // Injecting RabbitMQ service
        }

        public async Task AddCasinoWagerAsync(CasinoWagerPackage package)
        {
            if (package == null) throw new ArgumentNullException(nameof(package));

            // Check for existing player
            var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.Username == package.username);
            if (player == null)
            {
                player = new Players
                {
                    PlayerId = Guid.NewGuid(),
                    Username = package.username
                };

                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("New player created: {Username}", package.username);
            }

            //No need to pass guids in the request/body as they should always be unique
            var casinoWager = new CasinoWagers
            {
                WagerId = Guid.NewGuid(),
                Theme = package.theme,
                Provider = package.provider,
                GameName = package.gameName,
                TransactionId = Guid.NewGuid(),
                BrandId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Username = package.username,
                ExternalReferenceId = Guid.NewGuid(),
                TransactionTypeId = Guid.NewGuid(),
                Amount = (decimal)package.amount,
                CreatedDateTime = package.createdDateTime,
                NumberOfBets = package.numberOfBets,
                CountryCode = package.countryCode,
                SessionData = package.sessionData,
                Duration = package.duration,
                PlayerId = player.PlayerId
            };

            await _dbContext.CasinoWagers.AddAsync(casinoWager);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Successfully added casino wager for {Username}.", package.username);

            // Publish message to RabbitMQ
            _rabbitMqService.PublishMessage(package);
            _logger.LogInformation("Successfully published wager message to RabbitMQ for {Username}.", package.username);
        }

        public async Task<(List<CasinoWagersResponse> wagers, int total, int totalPages)> GetCasinoWagersAsync(Guid playerId, int pageSize, int page)
        {
            try
            {
                var wagers = await _dbContext.CasinoWagers
                    .Where(cas => cas.PlayerId == playerId)
                    .OrderByDescending(pla => pla.CreatedDateTime)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new CasinoWagersResponse
                    {
                        wagerId = x.WagerId,
                        gameName = x.GameName,
                        provider = x.Provider,
                        amount = x.Amount,
                        createdDateTime = x.CreatedDateTime
                    })
                    .ToListAsync();

                var total = await _dbContext.CasinoWagers.CountAsync(cas => cas.AccountId == playerId);
                var totalPages = (int)Math.Ceiling((double)total / pageSize);

                return (wagers, total, totalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the data for playerId: {PlayerId}", playerId);
                throw; // Re-throw exception to be handled in the controller
            }
        }

        public async Task<List<TopSpenderResponse>> GetTopSpendersAsync(int count)
        { 
            try
            {
                var topSpenders = await _dbContext.CasinoWagers
                    .GroupBy(x => new { x.AccountId, x.Username })
                    .Select(x => new TopSpenderResponse
                    {
                        AccountId = x.Key.AccountId,
                        Username = x.Key.Username,
                        TotalAmountSpend = x.Sum(x => x.Amount)
                    })
                    .OrderByDescending(x => x.TotalAmountSpend)
                    .Take(count)
                    .ToListAsync();

                return topSpenders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching top spenders.");
                throw; // Re-throw the exception for the controller to handle
            }
        }
    }
}
