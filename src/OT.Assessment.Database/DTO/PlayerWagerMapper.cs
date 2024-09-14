using OT.Assessment.Database.Tables;
using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Database.DTO
{
    public static class PlayerWagerMapper
    {
        public static PlayerWagers MapTo(Wagers wagers)
        {
            return new PlayerWagers
            {
                AccountId = wagers.AccountId,
                Amount = wagers.Amount,
                CountryCode = wagers.CountryCode,
                DateCreated = wagers.DateCreated,
                Duration = wagers.Duration,
                ExternalReferenceId = wagers.ExternalReferenceId,
                GameId = wagers.GameId,
                NumberOfBets = wagers.NumberOfBets,
                SessionId = wagers.SessionId,
                TransactionId = wagers.TransactionId,
                TransactionTypeId = wagers.TransactionTypeId,
                WagerId = wagers.WagerId
            };
        }

        public static List<PlayerWagers> ListMapTo(IEnumerable<Wagers> wagers)
        {
            return wagers.ToList().ConvertAll(new Converter<Wagers, PlayerWagers>(MapTo));
        }
    }
}
