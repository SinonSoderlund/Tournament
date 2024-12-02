using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGameRepository GameRepository { get; }

        public ITournamentRepository TournamentRepository { get; }

        private TournamentAPIContext context;

        public UnitOfWork(TournamentAPIContext context) 
        { 
            this.context = context;
            GameRepository = new GameRepository(context);
            TournamentRepository = new TournamentRepository(context);
        }

        public async Task CompleteAsync()
        {
           await context.SaveChangesAsync();
        }
    }
}
