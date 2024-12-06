using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.TournamentRequests
{
    public class TournamentUpdateRequest : RequestWithValidation
    {

        public TournamentUpdateDto TournamentUpdateDto { get; set; }



        public TournamentUpdateRequest(IAPIErrorSystem errorSystem, Func<object, bool> validation, TournamentUpdateDto tournamentUpdateDto = null!) : base(errorSystem, validation)
        {
            TournamentUpdateDto = tournamentUpdateDto;
        }
    }
}
