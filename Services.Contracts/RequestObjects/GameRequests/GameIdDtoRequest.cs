using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;

namespace Service.Contracts.RequestObjects.GameRequests
{
    public class GameIdDtoRequest : RequestBase
    {
        public GameIdDto GameIdDto { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }

        public GameIdDtoRequest(IAPIErrorSystem errorSystem, int id, string? name = null!, GameIdDto gameIdDto = null!) : base(errorSystem)
        {
            GameIdDto = gameIdDto;
            Id = id;
            Name = name;
        }
    }
}
