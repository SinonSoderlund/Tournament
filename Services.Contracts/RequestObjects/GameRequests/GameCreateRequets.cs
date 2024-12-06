using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.GameRequests
{
    public class GameCreateRequest : RequestWithValidation
    {
        public GameCreateDto GamecreateDto { get; set; }

        public GameCreateRequest(IAPIErrorSystem errorSystem, Func<object, bool> validate, GameCreateDto gameCreateDto = null!) : base(errorSystem, validate)
        {
            GamecreateDto = gameCreateDto;
        }
    }
}
