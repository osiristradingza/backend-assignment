using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Wagers.Interface
{
    public interface IWagerManager
    {
        Task<IEnumerable<PlayerWagers>> GetPlayerWagersAsync();
    }
}
