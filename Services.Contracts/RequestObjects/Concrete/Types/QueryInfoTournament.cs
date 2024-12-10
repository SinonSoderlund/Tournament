using Service.Contracts.RequestObjects.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.Concrete.Types
{
    public class QueryInfoTournament : IQueryInfo
    {
        public int Id { get; set; }
        public bool? includeGames { get; set; }

        public QueryInfoTournament(int id, bool? includeGames)
        {
            Id = id;
            this.includeGames = includeGames;
        }
    }
}
