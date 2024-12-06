using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.GameRequests
{
    public class IEnumerableGameIdDtoRequest : RequestBase
    {
        public IEnumerable<GameIdDto> GameIdDtos { get; set; }

        public IEnumerableGameIdDtoRequest(IAPIErrorSystem errorSystem, IEnumerable<GameIdDto> gameIdDto = null!) : base(errorSystem)
        {
            GameIdDtos = gameIdDto;
        }
    }
}
