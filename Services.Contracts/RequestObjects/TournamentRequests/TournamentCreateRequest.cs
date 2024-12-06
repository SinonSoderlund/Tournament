using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.TournamentRequests
{
    public class TournamentCreateRequest : RequestWithValidation
    {
        public TournamentCreateDto TournamentcreateDto { get; set; }

        public TournamentCreateRequest(IAPIErrorSystem errorSystem, Func<object, bool> validation, TournamentCreateDto tournamentCreateDto = null!) : base(errorSystem, validation)
        {
            TournamentcreateDto = tournamentCreateDto;
        }
    }
}
