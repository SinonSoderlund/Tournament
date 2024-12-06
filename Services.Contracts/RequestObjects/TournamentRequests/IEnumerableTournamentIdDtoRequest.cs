using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.TournamentRequests
{
    public class IEnumerableTournamentIdDtoRequest : RequestBase
    {
        public IEnumerable<TournamentIdDto> TournamentIdDtos { get; set; }

        public IEnumerableTournamentIdDtoRequest(IAPIErrorSystem errorSystem, IEnumerable<TournamentIdDto> tournamentIdDto = null!) : base(errorSystem)
        {
            TournamentIdDtos = tournamentIdDto;
        }
    }
}
