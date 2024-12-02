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
    internal class GameRepository : IGameRepository
    {
        private TournamentAPIContext context;
        public GameRepository(TournamentAPIContext context) 
        {
            this.context = context;
        }
        public void Add(Game game)
        {
            context.Add(game);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.Game.FirstOrDefaultAsync(x => x.Id == id) != default;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await context.Game.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await context.Game.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Game game)
        {
            context.Remove(game);
        }

        public void Update(Game game)
        {
            context.Game.Update(game);
        }
    }
}
