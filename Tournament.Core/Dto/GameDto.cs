﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public class GameDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Title lenght may not exceed 20 chaarcters")]
        public string Title { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public int TournamentId { get; set; }
    }
}
