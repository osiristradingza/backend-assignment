using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Games.Interface
{
    public interface IGameManager
    {
        Task<Guid?> AddProvider(AddProviderRequest addProviderRequest);
        Task<Guid?> AddGame(AddGameRequest addGameRequest);
    }
}
