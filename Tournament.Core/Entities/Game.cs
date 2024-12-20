﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Title lenght may not exceed 20 chaarcters")]
        public string Title { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public TournamentDetails Tournament {  get; set; }
    }
}
