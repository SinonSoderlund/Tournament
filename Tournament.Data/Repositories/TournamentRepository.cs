using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;


namespace Tournament.Data.Repositories
{
    internal class TournamentRepository : ITournamentRepository
    {
        private TournamentAPIContext context;
        public TournamentRepository(TournamentAPIContext tournamentAPIContext)
        {
            this.context = tournamentAPIContext;
        }
        public void Add(TournamentDetails tournamentDetails)
        {
            context.Add(tournamentDetails);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.TournamentDetails.FirstOrDefaultAsync(t => t.Id == id) != default;
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync()
        {
            return await context.TournamentDetails.ToListAsync();
        }

        public async Task<TournamentDetails> GetAsync(int id, bool includeGames = false)
        {
            return includeGames ? await context.TournamentDetails.Include(g => g.Games).FirstOrDefaultAsync(g => g.Id == id)
                   : await context.TournamentDetails.FirstOrDefaultAsync(g => g.Id == id);
        }

        public void Remove(TournamentDetails tournamentDetails)
        {
            context.Remove(tournamentDetails);
        }

        public void Update(TournamentDetails tournamentDetails)
        {
            context.Update(tournamentDetails);
        }
    }
}
