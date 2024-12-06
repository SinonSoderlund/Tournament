using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.TournamentRequests
{
    public class TournamentIdDtoRequest : RequestBase
    {
        public TournamentIdDto TournamentIdDto { get; set; }
        public int Id { get; set; }
        public bool? includeGames { get; set; }

        public TournamentIdDtoRequest(IAPIErrorSystem errorSystem, int id, bool? includegames = null!, TournamentIdDto tournamentIdDto = null!) : base(errorSystem)
        {
            TournamentIdDto = tournamentIdDto;
            Id = id;
            includeGames = includegames;
        }
    }
}
