using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.GameRequests
{
    public class GameUpdateRequest : RequestWithValidation
    {

        public GameUpdateDto GameUpdateDto { get; set; }



        public GameUpdateRequest(IAPIErrorSystem errorSystem, Func<object, bool> validate, GameUpdateDto gameUpdateDto = null!) : base(errorSystem, validate)
        {
            GameUpdateDto = gameUpdateDto;
        }

    }
}
