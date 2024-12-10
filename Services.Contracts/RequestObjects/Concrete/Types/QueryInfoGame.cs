using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts.RequestObjects.Interfaces.Types;

namespace Service.Contracts.RequestObjects.ConcreteType.Types
{
    public class QueryInfoGame : IQueryInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public QueryInfoGame(int id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
