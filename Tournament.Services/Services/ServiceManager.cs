using Service.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        public ITournamentService TournamnetService => tournamentService.Value;

        public IGameService GameService => gameService.Value;

        private readonly Lazy<ITournamentService> tournamentService;

        private readonly Lazy<IGameService> gameService;

        public ServiceManager(Lazy<ITournamentService> tournamentservice, Lazy<IGameService> gameservice)
        {
            tournamentService = tournamentservice;
            gameService = gameservice;
        }
    }
}
