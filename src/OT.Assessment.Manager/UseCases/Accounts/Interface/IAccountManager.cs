using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Accounts.Interfaces
{
    public interface IAccountManager
    {
        Task<string> AddAccountAsync(AddAccountRequest addAccountRequest, bool UseMassages = false);
        Task<Guid?> AddAccountAsync(AddAccountRequest addAccountRequest);
    }
}
