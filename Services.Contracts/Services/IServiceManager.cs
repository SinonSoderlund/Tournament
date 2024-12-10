﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Services
{
    public interface IServiceManager
    {
        public ITournamentService TournamentService { get; }
        public IGameService GameService { get; }

    }
}
