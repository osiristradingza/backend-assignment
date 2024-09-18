using Microsoft.Extensions.Logging;
using OT.Assessment.Database.Interface;
using OT.Assessment.Domain.Interface;
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
        private readonly IGlobalConfiguration _globalConfiguration;
        public AccountManager(ILogger<AccountManager> logger, IProducerService producerService, IAccounts accounts, IGlobalConfiguration globalConfiguration)
        {
            _logger = logger;
            _producerService = producerService;
            _accounts = accounts;
            _globalConfiguration = globalConfiguration;
        }


        public async Task<string> GlobalAddAccountAsync(AddAccountRequest addAccountRequest)
        {
            try
            {
                if (_globalConfiguration.UseMessaging)
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GlobalAddAccountAsync)} - massaging switched on.");
                    return await AddAccountAsync(addAccountRequest, true);
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GlobalAddAccountAsync)} - massaging switched off.");
                    var addAccount = await AddAccountAsync(addAccountRequest);
                    return addAccount.ToString() ?? "";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(GlobalAddAccountAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
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
        public async Task<string> AddCountryAsync(AddCountryRequest addCountryRequest, bool UseMassages)
        {
            try
            {
                if (UseMassages)
                {
                    await _producerService.PublishToCountryQueueAsync(addCountryRequest);
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(AddCountryAsync)} - {Nofications.SuccessfulPublishedAccountMessage}");
                    return Nofications.SuccessfulPublishedCountryMessage;
                }
                else
                    return Nofications.MessagingIsDisabled;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(AddCountryAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }
        public async Task<string> GlobalAddCountryAsync(AddCountryRequest addCountryRequest)
        {
            try
            {
                if (_globalConfiguration.UseMessaging)
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GlobalAddCountryAsync)} - massaging switched on.");
                    return await AddCountryAsync(addCountryRequest, true);
                }
                else
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GlobalAddCountryAsync)} - massaging switched off.");
                    var addAccount = await AddCountryAsync(addCountryRequest);
                    return addAccount.ToString() ?? "";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(GlobalAddCountryAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }
        public async Task<Guid?> AddCountryAsync(AddCountryRequest addCountryRequest)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(AddCountryAsync)} - attempting to add a country named {addCountryRequest.CountryName}.");
                return await _accounts.AddCountryAsync(addCountryRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(AddCountryAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }

        public async Task<IEnumerable<ApplicationPlayersResponse>> GetAllPlayersAsync() 
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GetAllPlayersAsync)} - attempting to get all players.");
                return await _accounts.GetAllPlayersAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(GetAllPlayersAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }

        public async Task<IEnumerable<ApplicationCountriesResponse>> GetAllCountriesAsync()
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now} - {nameof(AccountManager)} - {nameof(GetAllCountriesAsync)} - attempting to get all countries.");
                return await _accounts.GetAllCountriesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(AccountManager)} - {nameof(GetAllCountriesAsync)} - {ex.Message}");
                throw new Exception(Nofications.GeneralExceptionMessage);
            }
        }
    }
}
