using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Manager.UseCases.Accounts.Interfaces;
using OT.Assessment.Model;

namespace OT.Assessment.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        [Route("AddAccount")]
        public async Task<IActionResult> AddAccount(AddAccountRequest addAccountRequest) => Ok(await _accountManager.GlobalAddAccountAsync(addAccountRequest));

        [HttpPost]
        [Route("AddCountry")]
        public async Task<IActionResult> AddCountry(AddCountryRequest addCountryRequest ) => Ok(await _accountManager.GlobalAddCountryAsync(addCountryRequest));


    }
}
