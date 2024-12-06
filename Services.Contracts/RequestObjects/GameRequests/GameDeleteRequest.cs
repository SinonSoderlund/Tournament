using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.GameRequests
{
    public class GameDeleteRequest : RequestBase
    {
        public int Id { get; set; }

        public GameDeleteRequest(IAPIErrorSystem errorSystem, int id) : base(errorSystem)
        {
            Id = id;
        }
    }
}
