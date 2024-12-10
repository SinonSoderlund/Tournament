﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public class GameDto : DtoBase
    {
        public DateTime StartTime { get; set; }
        public int TournamentId { get; set; }
    }
}
