using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Bogus;


namespace Tournament.Data.Data
{
    public static class SeedData
    {
        /// <summary>
        /// Generates seed data for the Tournaments database
        /// </summary>
        /// <param name="nrOfTournaments">Number of tournaments to be generated.</param>
        /// <param name="minGames">Minimum number of games to be generated per tournament. Inclusive Value</param>
        /// <param name="maxGames">Maximum number of games to be generated per tournament. If MaxGames is less than or equal to MinGames then MinGames number of games will be generated. Inclusive Value</param>
        /// <returns>List of tournaments.</returns>
        public static IEnumerable<TournamentDetails> GenerateSeedData(uint nrOfTournaments, uint minGames, uint maxGames)
        {
            //throw if data makes no sense
            if (nrOfTournaments == 0)
                throw new ArgumentException("Number of Tournaments cannot be 0");
            else if (minGames == 0)
                throw new ArgumentException("Number of Games (MinGames) cannot be 0");
            else if(maxGames == int.MaxValue)
                throw new ArgumentException("Number of Games (MaxGames) cannot exceed or equal Int.MaxValue");


            Faker faker = new Faker();
            Random random = new Random();
            List<TournamentDetails> tours = new List<TournamentDetails>();

            //are number of games to be randomized
            bool isRandom = minGames < maxGames;
            
            for (int i = 0; i < nrOfTournaments; i++)
            {
                TournamentDetails t = new TournamentDetails()
                {
                    Title = faker.Commerce.ProductMaterial(),
                    StartDate = faker.Date.Future(3),
                };
                //only randomize number of games if that is to be done
                int v = !isRandom ? (int)minGames : random.Next((int)minGames, (int)maxGames+1);
                for(int j = 0; j < v; j++)
                {

                    //create a second date from tournament.starttime, between which games take place
                    DateTime next = t.StartDate;
                    next.AddMonths(3);
                    Game g = new Game()
                    {
                        Title = faker.Commerce.ProductMaterial(),
                        Time = faker.Date.Between(t.StartDate, next),
                    };
                    t.Games.Add(g);
                }
                tours.Add(t);
            }

            return tours;
        }
    }
}
