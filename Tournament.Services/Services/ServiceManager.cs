using Service.Contracts.Services;

namespace Tournament.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        public ITournamentService TournamentService { get; }

        public IGameService GameService { get; }


        public ServiceManager(ITournamentService tournamentservice,IGameService gameservice)
        {
            TournamentService = tournamentservice;
            GameService = gameservice;
        }
    }
}
