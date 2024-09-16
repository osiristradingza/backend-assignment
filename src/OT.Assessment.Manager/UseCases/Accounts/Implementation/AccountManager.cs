using Microsoft.Extensions.Logging;
using OT.Assessment.Database.Interface;
using OT.Assessment.Manager.UseCases.Accounts.Interfaces;
using OT.Assessment.Manager.UseCases.Games.Implementation;
using OT.Assessment.Messaging.Producer.Interface;
using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Accounts.Repository
{
    public class AccountManager : IAccountManager
    {
        private readonly ILogger<AccountManager> _logger;
        private readonly IProducerService _producerService;
        private readonly IAccounts _accounts;
        public AccountManager(ILogger<AccountManager> logger, IProducerService producerService, IAccounts accounts)
        {
            _logger = logger;
            _producerService = producerService;
            _accounts = accounts;   
        }

        public async Task<string> AddAccountAsync(AddAccountRequest addAccountRequest, bool UseMassages)
        {
            try
            {
                if (UseMassages)
                {
                    await _producerService.PublishToAccountQueueAsync(addAccountRequest);
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(AddAccountAsync)} - {Nofications.SuccessfulPublishedAccountMessage}");
                    return Nofications.SuccessfulPublishedAccountMessage;
                }
                else
                    return Nofications.MessagingIsDisabled;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(AddAccountAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }

        public async Task<Guid?> AddAccountAsync(AddAccountRequest addAccountRequest)
        {
            try 
            {
                _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(AddAccountAsync)} - attempting to add a account for {addAccountRequest.FirstName}.");
                return await _accounts.AddAccountAsync(addAccountRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(AddAccountAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }

        }
    }
}
