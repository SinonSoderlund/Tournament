using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public class TournamentDto : DtoBase
    {

        public DateTime EndTime { get => StartTime.AddMonths(3); }

        public ICollection<GameIdDto> Games = [];

    }
}
